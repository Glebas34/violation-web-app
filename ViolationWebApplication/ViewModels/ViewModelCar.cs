using System.ComponentModel.DataAnnotations;
namespace ViolationWebApplication.ViewModels
{
    public class ViewModelCar
    {
        [Required(ErrorMessage = "Укажите производителя машины")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Укажите модель машины")]
        public string Model { get; set; }
        [RegularExpression(@"[0-9]{10}")]
        [Required(ErrorMessage ="Укажите номер водительских прав")]
        public string DriversLicense { get; set; }
    }
}
