using System.ComponentModel.DataAnnotations;
namespace ViolationWebApplication.ViewModels
{
    public class ViewModelCar
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "Укажите номер машины")]
        public string CarNumber { get; set; }

        [Required(ErrorMessage = "Укажите производителя машины")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Укажите модель машины")]
        public string Model { get; set; }

        [RegularExpression(@"[0-9]{10}",ErrorMessage = "Номер паспорта владельца состоит из 10 цифр")]
        [Required(ErrorMessage ="Укажите номер паспорта владельца")]
        public string Passport { get; set; }
        [Required(ErrorMessage = "Укажите ФИО владельца")]
        public string FullName { get; set; }
    }
}
