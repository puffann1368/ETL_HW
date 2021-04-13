using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETLAPI.Model{
    public class LineItem_09{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{get;set;}
        [MaxLength(50)]
        [Required]
        [ForeignKey("Account")]
        public string AccountPayerAccountId{get;set;}
        [Required]
        [ForeignKey("Production")]
        public int ProductionID{get;set;}
        [MaxLength(50)]
        public string LineItemType{get;set;}
        public decimal UsageAmount{get;set;}
        public decimal UnblendedRate{get;set;}
        public decimal UnblendedCost{get;set;}
        [Required]
        public DateTime Date{get;set;}
        public DateTime UsageStartDate{get;set;}
        public DateTime UsageEndDate{get;set;}
        
    }
}