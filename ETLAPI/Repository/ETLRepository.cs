using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETLAPI.Model;
using ETLAPI.Component;
using ETLAPI.Interface.IRepository;
using Microsoft.Extensions.Logging;
using Dapper;

namespace ETLAPI.Repository
{
    public class ETLRepository: IETLRepository
    {
        private readonly ILogger<ETLRepository> _logger;

        private readonly DbContextOptions<ETLDbContext> _options;
        private readonly IDbConnection _connection;

        public ETLRepository(ILogger<ETLRepository> logger,DbContextOptions<ETLDbContext> options,IDbConnection connection)
        {
            _logger = logger;
            _options = options;
            _connection = connection;
        }

        public async Task<IEnumerable<UnblendedCostInfo>> GetSumUnblendedCost(string AccountID, DateTime StartDate, DateTime EndDate){
            try{
                    // using(var context = new ETLDbContext(_options)){
                    //     var uTable;
                    //     switch(StartDate.Month){
                    //         case 1:
                    //             uTable = context.LineItems_01;
                    //     }                      
                    //     var groupData = 
                    //                from ac in context.DbSet<LineItem_01>().ToLinqToDBTable().TableName($"LineItems_{TableIndex}")
                    //                      join o in context.Productions on ac.ProductionID equals o.ProductionID
                    //                      where ac.AccountPayerAccountId == AccountID && (ac.Date >= StartDate && ac.Date < EndDate) 
                    //                      group ac by o.ProductionName into g 
                    //                      select new UnblendedCostInfo{
                    //                         ProductionName = g.Key,
                    //                         SumOfUnblededCost = g.Sum(i => i.UnblendedCost)
                    //                      };
                    //     return await groupData.ToListAsync();

                    // }
                    string TableIndex = StartDate.Month.ToString().PadLeft(2,'0');
                    using (var conn = _connection)
                    {
                        string sQuery = $@"SELECT ProductionName,SUM(UnblendedCost) AS SumOfUnblededCost 
                                          FROM ETL.DBO.LineItems_{TableIndex} LT
                                          INNER JOIN ETL.DBO.Productions P ON LT.ProductionID = P.ProductionID
                                          WHERE LT.AccountPayerAccountId = @AccountID AND LT.UsageStartDate >=@StartDate AND LT.UsageStartDate<=@EndDate
                                          GROUP BY P.ProductionName";
                        conn.Open();
                        var result = await conn.QueryAsync<UnblendedCostInfo>(sQuery, new {AccountID = AccountID,StartDate=StartDate,EndDate=EndDate });
                        return result.ToList();
                    }
            }catch(Exception ex){
                _logger.LogError($"GetSumUnblendedCost In Repo got errors:{ex.Message}");
                throw ex;
            }
        }

        public async Task<IEnumerable<UsageAmoutInfo>> GetSumUsageAmount(string AccountID, DateTime StartDate, DateTime EndDate){
            try{
                 string TableIndex = StartDate.Month.ToString().PadLeft(2,'0');
                    using (var conn = _connection)
                    {
                        string sQuery = $@"SELECT ProductionName,LT.UsageStartDate AS Date,SUM(UsageAmount) AS SumOfUsageAmount 
                                          FROM ETL.DBO.LineItems_{TableIndex} LT
                                          INNER JOIN ETL.DBO.Productions P ON LT.ProductionID = P.ProductionID
                                          WHERE LT.AccountPayerAccountId = @AccountID AND LT.UsageStartDate >=@StartDate AND LT.UsageStartDate<=@EndDate
                                          GROUP BY P.ProductionName,LT.UsageStartDate
                                          ORDER BY ProductionName";
                        conn.Open();
                        var result = await conn.QueryAsync<UsageAmoutInfo>(sQuery, new {AccountID = AccountID,StartDate=StartDate,EndDate=EndDate });
                        return result.ToList();
                    }
            }catch(Exception ex){
                _logger.LogError($"GetSumUsageAmount In Repo got errors:{ex.Message}");
                throw ex;
            }
        }

       

    }
}