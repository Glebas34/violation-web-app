using System.ComponentModel.DataAnnotations;

namespace ViolationWebApplication.ViewModels
{
    public class ViewModelOwner
    {
        [Required(ErrorMessage = "Укажите фамилию владельца")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Укажите имя владельца")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите отчество владельца")]
        public string Patronymic { get; set; }
    }
}
