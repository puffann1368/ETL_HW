using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ETLProcess.Component;
using ETLProcess.Model;
using System.Collections.Concurrent;
using ETLProcess.Interface;
using ETLProcess.Interface.IRepository;

namespace ETLProcess.Services
{
    public class Processor: IETLProcessor
    {
        private readonly ILogger _logger;
        private BlockingCollection<DataInfo> bc = new BlockingCollection<DataInfo>();
        private CancellationToken _stoppingToken;
        private string _sourcePath;
        private ConcurrentDictionary<string,List<LineItemDto>> _recordMapping = new ConcurrentDictionary<string, List<LineItemDto>>();
        private BlockingCollection<DataInfo> _needtransformBC = new BlockingCollection<DataInfo>();
        private IProductionRepo _IProdRepo;

        private ILineItemRepo _ILineItemRepo;
        
        public Processor(ILogger logger,IProductionRepo IprodRepo,ILineItemRepo ILineItemRepo)
        {
            _logger = logger;
            _IProdRepo = IprodRepo;
            _ILineItemRepo = ILineItemRepo;
        }


        public async Task Run(string SourcePath,CancellationToken StoppingToken){
            try{
                _sourcePath = SourcePath;
                _stoppingToken = StoppingToken;
                //ADD Task to Collect DataInfo.
                Task CollectDataInfoTask = Task.Run(CollectDataInfo);
                //ADD Task to Extract Data.
                Task ExtractDataTask = Task.Run(ExtractData);
                //ADD Task to Transform.
                await Task.Run(Transformation); 
                

            }catch(Exception ex){
                _logger.LogError(ex.Message);
            }
        }

        public void CollectDataInfo(){
            try{
                foreach (DataInfo item in GetFileInfo(_sourcePath)){
                        bool isSuccess = false;
                        try {
                            isSuccess=bc.TryAdd(item, 5000, _stoppingToken);
                        } catch (OperationCanceledException ex) {
                            _logger.LogError("collect data is canceled.");
                            bc.CompleteAdding();
                            break;
                        } catch (Exception ex) {
                            _logger.LogError(ex.Message);
                            bc.CompleteAdding();
                            break;
                        }

                        if (!isSuccess) {
                            _logger.LogError("add extract data is block.");
                        } 
                        _logger.LogInformation(item.AccountID);
                }
                bc.CompleteAdding();
             }catch(Exception ex){
                _logger.LogError(ex.Message);
            }
        }

        public IEnumerable<DataInfo> GetFileInfo(string SourcePath){
              //step 1. Get files in folder to list
                string[] FilePaths = Directory.GetFiles(SourcePath, "*.csv",
                                         SearchOption.AllDirectories);
                foreach(string File in FilePaths){
                    DateTime Date;
                    //TODO:checkfile
                    string FileName = Path.GetFileName(File);
                    //get file info.
                    string[] FileInfo = FileName.Split('-');
                    string AccountID = FileInfo[0];
                    int Year,Mouth,Day;
                    if(!int.TryParse(FileInfo[1],out Year) || !int.TryParse(FileInfo[2],out Mouth) || !int.TryParse(FileInfo[3],out Day)){
                        _logger.LogError("date format of file is not correct(not numeric).");
                        yield break;
                    }
                    if(!DateTime.TryParse($"{FileInfo[1]}-{FileInfo[2]}-{FileInfo[3]}",out Date)){
                        _logger.LogError("date format of file is not correct(not date).");
                        yield break;
                    }
                    yield return new DataInfo(File,AccountID,Date);
                }
        }

        public void ExtractData(){
            try{
                while (!bc.IsCompleted) {
                    DataInfo data;
                    try {
                        if (!bc.TryTake(out data, 5000, _stoppingToken)) {
                            _logger.LogInformation("no data needs extract.");
                        } else {
                            decimal decValue;

                            List<LineItemDto> results = System.IO.File.ReadAllLines(data.FilePath)
                                            .AsParallel()
                                            .WithDegreeOfParallelism(2)
                                            .Select(x => x.Split(','))
                                            .Skip(1)
                                            .Select(y=>new LineItemDto(){
                                                UsageAccountId =data.AccountID,
                                                LineItemType = y[8],
                                                UsageAmount =decimal.TryParse(y[15],out decValue)? Convert.ToDecimal(y[15]) : 0,
                                                UnblendedRate = decimal.TryParse(y[18],out decValue)? Convert.ToDecimal(y[18]) : 0,
                                                UnblendedCost = decimal.TryParse(y[19],out decValue)? Convert.ToDecimal(y[19]) : 0,
                                                UsageStartDate = Convert.ToDateTime(y[9]),
                                                UsageEndDate = Convert.ToDateTime(y[10]),
                                                ProductName =y[21]
                                            }).ToList<LineItemDto>();
                            bool isSuccess = false;
                            try {
                                //add to mapping.
                                _recordMapping.TryAdd(data.AccountID,results);       
                                isSuccess=_needtransformBC.TryAdd(data, 5000, _stoppingToken);
                            } catch (OperationCanceledException ex) {
                                _logger.LogError("extractData is canceled");
                                _needtransformBC.CompleteAdding();
                                break;
                            } catch (Exception ex) {
                                _logger.LogError(ex.Message);
                                _needtransformBC.CompleteAdding();
                                break;
                            }

                            if (!isSuccess) {
                                _logger.LogError("add transform bc is block. ");
                            } 
                            _logger.LogInformation(data.AccountID);
                        }
                    } catch (OperationCanceledException ex) {
                        _logger.LogInformation("extractData is canceled");
                        break;
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    Thread.Sleep(3000);
                }
                _needtransformBC.CompleteAdding();
            }catch(Exception ex){
                _logger.LogError(ex.Message);
            }
        } 


        public async Task Transformation(){
            try{
                while (!_needtransformBC.IsCompleted) {
                  try {
                        DataInfo DataRecord;
                        if (!_needtransformBC.TryTake(out DataRecord, 5000, _stoppingToken)) {
                            _logger.LogInformation("no data needs transform.");
                        } else {
                            
                            _logger.LogInformation($"start to transform {DataRecord.AccountID}");

                            //add and get keyvalue of Productions
                            List<string> prods =  _recordMapping[DataRecord.AccountID]
                              .GroupBy(c=>c.ProductName.Trim().Replace("\"",""))
                              .Select(x=>x.Key).ToList();
                            Dictionary<string,int> dictionary =  _IProdRepo.Add(prods).Result.ToDictionary(data => data.Key,data=>data.Value);

                            //group by startdate and enddate
                            List<LineItemDto> result = _recordMapping[DataRecord.AccountID]
                                         .GroupBy(c => new
                                                        {
                                                            c.LineItemType,
                                                            c.UsageStartDate,
                                                            c.UsageEndDate,
                                                            c.ProductName
                                                        })
                                         .Select(k => new LineItemDto()
                                                {
                                                    UsageAccountId = DataRecord.AccountID,
                                                    LineItemType = k.Key.LineItemType,
                                                    UsageStartDate = k.Key.UsageStartDate,
                                                    UsageEndDate = k.Key.UsageEndDate,
                                                    ProductName = k.Key.ProductName,
                                                    ProductID = dictionary[k.Key.ProductName.Trim().Replace("\"","")],
                                                    UsageAmount = k.Sum(x=>x.UsageAmount),
                                                    UnblendedRate = k.Sum(x=>x.UnblendedRate),
                                                    UnblendedCost = k.Sum(x=>x.UnblendedCost),
                                                    Date = DataRecord.Date
                                                }).ToList<LineItemDto>();
                                
                                _logger.LogInformation($"transform {DataRecord.AccountID} is completed.");

                                //bulk insert to db.
                                await _ILineItemRepo.BulkInsert(result, DataRecord.Date,DataRecord.AccountID);
                        }
                    } catch (OperationCanceledException ex) {
                        _logger.LogInformation("transformation is canceled");
                        break;
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                        break;
                    }

                }
            }catch(Exception ex){
                _logger.LogError(ex.Message);
            }
        }
    }
}