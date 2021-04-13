using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETLProcess.Model{
    public class Production{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductionID {get;set;}
        [MaxLength(150)]
        [Required]
        public string ProductionName {get;set;}
        public List<LineItem_01> LineItem01List {get;set;} = new List<LineItem_01>();
        public List<LineItem_02> LineItem02List {get;set;} = new List<LineItem_02>();
        public List<LineItem_03> LineItem03List {get;set;} = new List<LineItem_03>();
        public List<LineItem_04> LineItem04List {get;set;} = new List<LineItem_04>();
        public List<LineItem_05> LineItem05List {get;set;} = new List<LineItem_05>();
        public List<LineItem_06> LineItem06List {get;set;} = new List<LineItem_06>();
        public List<LineItem_07> LineItem07List {get;set;} = new List<LineItem_07>();
        public List<LineItem_08> LineItem08List {get;set;} = new List<LineItem_08>();
        public List<LineItem_09> LineItem09List {get;set;} = new List<LineItem_09>();
        public List<LineItem_10> LineItem10List {get;set;} = new List<LineItem_10>();
        public List<LineItem_11> LineItem11List {get;set;} = new List<LineItem_11>();
        public List<LineItem_12> LineItem12List {get;set;} = new List<LineItem_12>();
    }
}