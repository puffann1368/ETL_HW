using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ETLAPI.Component;

namespace ETLAPI.Interface
{
    public interface IETLService
    {
        Task<CommonResult> GetSumUnblendedCost(string AccountID, DateTime StartDate, DateTime EndDate);
        Task<CommonResult> GetSumUsageAmount(string AccountID,DateTime StartDate, DateTime EndDate);
    }
}