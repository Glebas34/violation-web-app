using System.ComponentModel.DataAnnotations;
namespace ViolationWebApplication.ViewModels
{
    public class ViewModelLogin
    {
        [Display(Name = "Email адрес")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}