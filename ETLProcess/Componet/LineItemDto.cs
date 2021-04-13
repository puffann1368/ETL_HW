using System;

namespace ETLProcess.Component{
    public class LineItemDto{

        public string UsageAccountId{get;set;}
        public int ProductID{get;set;}
        public string ProductName{get;set;}
        public string LineItemType{get;set;}
        public decimal UsageAmount{get;set;}
        public decimal UnblendedRate{get;set;}
        public decimal UnblendedCost{get;set;}
        public DateTime Date{get;set;}
        public DateTime UsageStartDate{get;set;}
        public DateTime UsageEndDate{get;set;}
        
    }
}