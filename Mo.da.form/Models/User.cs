using System.ComponentModel.DataAnnotations;
namespace MO.DA.FORM.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string group { get; set; }
        public bool leader { get; set; }
    }
}



