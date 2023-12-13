using System.ComponentModel.DataAnnotations;

namespace MO.DA.FORM.Models
{
    public class Homework
    {
        [Key]
        public int id { get; set; }
        public string text { get; set; }  
        public string deadline { get; set; }  
        public string subject { get; set; }
    }
    
    public class HomeworkViewModel
    {
        [Key]
        public int id { get; set; }
        public string text { get; set; }

        [DataType(DataType.Date)]
        public DateTime deadline { get; set; }  
        public string subject { get; set; }
    }
}
