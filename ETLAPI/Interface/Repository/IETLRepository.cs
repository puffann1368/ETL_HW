using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ETLAPI.Component;

namespace ETLAPI.Interface.IRepository
{
    public interface IETLRepository
    {
        Task<IEnumerable<UnblendedCostInfo>> GetSumUnblendedCost(string AccountID, DateTime StartDate, DateTime EndDate);
        Task<IEnumerable<UsageAmoutInfo>> GetSumUsageAmount(string AccountID, DateTime StartDate, DateTime EndDate);
    }
}