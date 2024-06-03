using System.ComponentModel.DataAnnotations;

namespace ViolationWebApplication.ViewModels
{
    public class ViewModelAdmin
    {
        [Display(Name = "Фамилия")]
        //[RegularExpression(@"[А-Яа-яёЁ]", ErrorMessage = "Фамилия может содержать буквы русского алфавита")]
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите Имя")]
        //[RegularExpression(@"[А-Яа-яёЁ]", ErrorMessage = "Имя может содержать буквы русского алфавита")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        //[RegularExpression(@"[А-Яа-яёЁ]", ErrorMessage = "Отчество может содержать буквы русского алфавита")]
        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }

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