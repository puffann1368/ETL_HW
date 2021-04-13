
namespace ETLAPI.Component{
   public class CommonResult{
       public CommonResult(bool isSuccess, string errorMessage, object result){
           IsSuccess = isSuccess;
           ErrorMessage = errorMessage;
           Result = result;
       }
       public bool IsSuccess{get;set;}
       public string ErrorMessage{get;set;}
       public object Result{get;set;}
   }
}