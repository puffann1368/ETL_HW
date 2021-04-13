using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ETLAPI.Component;
using ETLAPI.Interface;


namespace ETLAPI.Controllers
{
    [ApiController]
    
    public class ETLController : ControllerBase
    {
        private readonly ILogger<ETLController> _logger;
        private readonly IETLService _IETLService;
        private static IMemoryCache _memoryCache;
        private readonly ICacheHelper _cacheHelper;
        public ETLController(ILogger<ETLController> logger, IETLService IETLService,IMemoryCache MemoryCache,ICacheHelper CacheHelpr)
        {
            _logger = logger;
            _IETLService = IETLService;
            _memoryCache = MemoryCache;
            _cacheHelper = CacheHelpr;
        }

        [HttpPost]
        [Route("[controller]/GetSumUBCost")]
        public async Task<ActionResult> GetSumUnblendedCost([FromBody] QueryForm Data)
        {
            CommonResult result = null;
            try{
                 string CacheKey = $"{Data.AccountID}-{Data.StartDate}-{Data.EndDate}";
                 string CacheResult = string.Empty;
                 if(_memoryCache.TryGetValue(CacheKey,out CacheResult)){
                     _memoryCache.Set(CacheKey, CacheResult, _cacheHelper.GetCacheEntryOptions());
                     return Ok(CacheResult);
                 }

                result = await _IETLService.GetSumUnblendedCost(Data.AccountID,Data.StartDate,Data.EndDate);
                if(result.IsSuccess == false)
                    return StatusCode(StatusCodes.Status500InternalServerError,new JsonResponseResult(result.IsSuccess,result.ErrorMessage,null));
                
                string JsonResult = JsonConvert.SerializeObject(result.Result);
                _memoryCache.Set(CacheKey, JsonResult, _cacheHelper.GetCacheEntryOptions());
              
                return Ok(JsonResult);
            }catch(Exception ex){
                _logger.LogError($"GetSumUnblendedCost In Controller got errors:{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,"系統發生未預期錯誤");
            }
        }

        [HttpPost]
        [Route("[controller]/GetSumUsageAmount")]
        public async Task<ActionResult> GetSumUsageAmount([FromBody] QueryForm Data)
        {
            CommonResult result = null;
            try{
                 string CacheKey = $"{Data.AccountID}-{Data.StartDate}-{Data.EndDate}";
                 string CacheResult = string.Empty;
                 if(_memoryCache.TryGetValue(CacheKey,out CacheResult)){
                     _memoryCache.Set(CacheKey, CacheResult, _cacheHelper.GetCacheEntryOptions());
                     return Ok(CacheResult);
                 }
                result = await _IETLService.GetSumUsageAmount(Data.AccountID,Data.StartDate,Data.EndDate);
                if(result.IsSuccess == false)
                    return StatusCode(StatusCodes.Status500InternalServerError,new JsonResponseResult(result.IsSuccess,result.ErrorMessage,null));

                string JsonResult = JsonConvert.SerializeObject(result.Result);
                _memoryCache.Set(CacheKey, JsonResult, _cacheHelper.GetCacheEntryOptions());
                    return Ok(JsonResult);
            }catch(Exception ex){
                _logger.LogError($"GetSumUsageAmount In Controller got errors:{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,"系統發生未預期錯誤");
            }
        }
    }
}
