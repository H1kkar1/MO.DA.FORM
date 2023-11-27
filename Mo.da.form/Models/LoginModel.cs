using System.ComponentModel.DataAnnotations;

namespace MO.DA.FORM.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Роль не указана")]
        public bool leader { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
