using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETLProcess.Model;
using ETLProcess.Component;
using ETLProcess.Interface.IRepository;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;


namespace ETLProcess.Repository
{
    public class LineItemRepository: ILineItemRepo
    {
        private readonly ILogger<LineItemRepository> _logger;

        private readonly DbContextOptions<ETLDbContext> _options;

        private readonly IDbConnection _connection;

        public LineItemRepository(IDbConnection connection,ILogger<LineItemRepository> logger,DbContextOptions<ETLDbContext> options)
        {
            _logger = logger;
            _options = options;
            _connection = connection;
        }

        public async Task BulkInsert(List<LineItemDto> lineItemDtos,DateTime Date,string AccountID){
            try{
                    DataTable sourceDt = new DataTable();
                    sourceDt.Columns.Add("Date", typeof(DateTime));
                    sourceDt.Columns.Add("AccountPayerAccountId", typeof(string));
                    sourceDt.Columns.Add("UsageAmount", typeof(decimal));
                    sourceDt.Columns.Add("UnblendedRate", typeof(decimal));
                    sourceDt.Columns.Add("UnblendedCost", typeof(decimal));
                    sourceDt.Columns.Add("UsageStartDate", typeof(DateTime));
                    sourceDt.Columns.Add("UsageEndDate", typeof(DateTime));
                    sourceDt.Columns.Add("ProductionID", typeof(string));
                    sourceDt.Columns.Add("LineItemType", typeof(string));

                    foreach(LineItemDto dto in lineItemDtos){
                        DataRow dr = sourceDt.NewRow();
                        dr["Date"] = dto.Date;
                        dr["AccountPayerAccountId"] = dto.UsageAccountId;
                        dr["UsageAmount"] = dto.UsageAmount;
                        dr["UnblendedRate"] = dto.UnblendedRate;
                        dr["UnblendedCost"] = dto.UnblendedCost;
                        dr["UsageStartDate"] = dto.UsageStartDate;
                        dr["UsageEndDate"] = dto.UsageEndDate;
                        dr["ProductionID"] = dto.ProductID;
                        dr["LineItemType"] = dto.LineItemType;
                        sourceDt.Rows.Add(dr);
                    }
                    
                        using(SqlConnection con = new SqlConnection(_connection.ConnectionString))
                        {
                            con.Open();
                            using (SqlTransaction transaction = con.BeginTransaction())
                            {
                                try{
                                    using (var cmd = con.CreateCommand())
                                    {
                                        cmd.CommandText = $"DELETE FROM LineItems_{Date.Month.ToString().PadLeft(2,'0')} WHERE Date = @Date AND AccountPayerAccountId=@AccountId";
                                        cmd.Transaction = transaction;
                                        cmd.Parameters.AddWithValue("@Date", Date);  
                                        cmd.Parameters.AddWithValue("@AccountId", AccountID);  
                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                    using (SqlBulkCopy dest = new SqlBulkCopy(con,SqlBulkCopyOptions.Default,transaction))
                                    {
                                        dest.DestinationTableName = $"dbo.LineItems_{Date.Month.ToString().PadLeft(2,'0')}";                
                                        dest.ColumnMappings.Add("Date", "Date");
                                        dest.ColumnMappings.Add("AccountPayerAccountId", "AccountPayerAccountId");
                                        dest.ColumnMappings.Add("UsageAmount", "UsageAmount");
                                        dest.ColumnMappings.Add("UnblendedRate", "UnblendedRate");
                                        dest.ColumnMappings.Add("UnblendedCost", "UnblendedCost");
                                        dest.ColumnMappings.Add("UsageStartDate", "UsageStartDate");
                                        dest.ColumnMappings.Add("UsageEndDate", "UsageEndDate");
                                        dest.ColumnMappings.Add("ProductionID", "ProductionID");
                                        dest.ColumnMappings.Add("LineItemType", "LineItemType");
                                        await dest.WriteToServerAsync(sourceDt);
                                    }
                                        await transaction.CommitAsync();
                                }catch(Exception ex){
                                    await transaction.RollbackAsync();
                                }
                            }
                        }
            }catch(Exception ex){
                _logger.LogError($"Add Production got errors:{ex.Message}");
                throw ex;
            }
        }
    }
}