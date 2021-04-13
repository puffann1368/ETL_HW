
namespace ETLAPI.Component{
   public class JsonResponseResult{
       public JsonResponseResult(bool isSuccess, string errorMessage, string result){
           IsSuccess = isSuccess;
           ErrorMessage = errorMessage;
           Result = result;
       }
       public bool IsSuccess{get;set;}
       public string ErrorMessage{get;set;}
       public string  Result{get;set;}
   }
}