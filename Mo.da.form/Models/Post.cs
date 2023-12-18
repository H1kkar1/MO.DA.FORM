using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MO.DA.FORM.Models
{
    public class Post
    {

        [Key]
        public int post_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string datetime { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public byte[]? file { get; set; }

    } 
    public class PostViewModel
    {

        [Key]
        public int post_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime datetime { get; set; }

       // [BindingBehavior(BindingBehavior.Optional)]
        public IFormFile? file { get; set; }

    }
}



