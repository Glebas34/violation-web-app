using System.ComponentModel.DataAnnotations;
namespace ViolationWebApplication.ViewModels
{
    public class ViewModelRegister
    {
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Имя пользователь")]
        [Required(ErrorMessage = "Введите имя пользователья")]
        public string UserName { get; set; }

        [Display(Name = "Номер водительских прав")]
        [RegularExpression(@"[0-9]{10}")]
        [Required(ErrorMessage = "Введите номер водительских прав")]
        public string DriversLicense { get; set; }

        [Display(Name = "Email адрес")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введите Email адрес")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите Пароль")]
        public string Password { get; set; }

        [Display(Name = "Подверждение Пароля")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Несовпадение паролей")]
        [Required(ErrorMessage = "Подтвердите Пароль")]
        public string ConfirmPassword { get; set; }

    }
}
