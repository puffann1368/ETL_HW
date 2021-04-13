using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ETLAPI.Interface.IRepository;
using ETLAPI.Interface;
using ETLAPI.Component;

namespace ETLAPI.Service{
    public class ETLService: IETLService{
        private readonly ILogger<ETLService> _logger;
        private readonly IETLRepository _IETLRepo;
        public ETLService(ILogger<ETLService> logger, IETLRepository IETLRepo)
        {
            _logger = logger;
            _IETLRepo = IETLRepo;
        }
        public async Task<CommonResult> GetSumUnblendedCost(string AccountID, DateTime StartDate, DateTime EndDate){
            try{
                if(StartDate.Month != EndDate.Month)
                    return new CommonResult(false,"日期需在相同月份",null);
                IEnumerable<UnblendedCostInfo> Result = await _IETLRepo.GetSumUnblendedCost(AccountID,StartDate,EndDate);
                Dictionary<string, decimal> SumOfCost = new Dictionary<string, decimal>();
                foreach(UnblendedCostInfo info in Result){
                    SumOfCost[info.ProductionName] = info.SumOfUnblededCost;
                }
                return new CommonResult(true,string.Empty,SumOfCost);
             }catch(Exception ex){
                _logger.LogError($"GetSumUnblendedCost In Service got errors:{ex.Message}");
                return new CommonResult(false,"系統發生未預期錯誤.",null);
            }
        }

        public async Task<CommonResult> GetSumUsageAmount(string AccountID,DateTime StartDate, DateTime EndDate){
            try{
                 if(StartDate.Month != EndDate.Month)
                    return new CommonResult(false,"日期需在相同月份",null);
                IEnumerable<UsageAmoutInfo> Result = await _IETLRepo.GetSumUsageAmount(AccountID,StartDate,EndDate);
                Dictionary<string, Dictionary<string,decimal>> SumOfCost = new Dictionary<string, Dictionary<string,decimal>>();
                //generate dictionary with days.
                var ProductionList =  Result.GroupBy(p=>p.ProductionName).Select(x=>x.Key).ToList();
                for(DateTime s = StartDate; s<=EndDate; s=s.AddDays(1)){
                    foreach(string p in ProductionList){
                        if(!SumOfCost.ContainsKey(p))
                            SumOfCost[p] = new Dictionary<string, decimal>();
                        SumOfCost[p].Add(s.ToString("yyyy/MM/dd"), 0m);
                    }
                }
                //update record of sum.
                foreach(UsageAmoutInfo info in Result){
                    SumOfCost[info.ProductionName][info.Date.ToString("yyyy/MM/dd")] = info.SumOfUsageAmount;
                }
                return new CommonResult(true,string.Empty,SumOfCost);
            }catch(Exception ex){
                _logger.LogError($"GetSumUsageAmount In Service got errors:{ex.Message}");
                return new CommonResult(false,"系統發生未預期錯誤.",null);
            }
        }

    }
}