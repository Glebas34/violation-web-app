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
        [RegularExpression(@"[a-zA-Z0-9_-]+", ErrorMessage = "Имя пользователя может содержать символы _, -, цифры и латинские буквы")]
        [MaxLength(20, ErrorMessage = "Имя пользователя может содержать не более 20 символов")]
        public string UserName { get; set; }

        [Display(Name = "Номер водительских прав")]
        [Required(ErrorMessage = "Введите номер водительских прав")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Номер водительских прав состоит из 10 цифр")]
        public string DriversLicense { get; set; }

        [Display(Name = "Email адрес")]
        [Required(ErrorMessage = "Введите Email адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подверждение Пароля")]
        [Required(ErrorMessage = "Подтвердите Пароль")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Несовпадение паролей")]
        public string ConfirmPassword { get; set; }

    }
}
