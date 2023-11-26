using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MO.DA.FORM.Models
{
    public class Post
    {

        [Key]
        public int post_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        
        [BindProperty, DataType(DataType.Date)]
        public DateOnly datetime { get; set; }
        public byte[] file { get; set; }

    } 
    public class PostViewModel
    {

        [Key]
        public int post_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        
        [BindProperty, DataType(DataType.Date)]
        public DateOnly datetime { get; set; }
        public IFormFile file { get; set; }

    }
}



