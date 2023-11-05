using System.ComponentModel.DataAnnotations;
using ViolationWebApplication.Data;

namespace ViolationWebApplication.ViewModels
{
    public class ViewModelViolation
    {
        [Required(ErrorMessage = "Выберите тип нарушения")]
        public ViolationType TypeOfViolation {  get; set; }

        [Required(ErrorMessage = "Введите сумму штрафа")]
        public int FineFee {  get; set; }

        [Required(ErrorMessage = "Укажите номер машины")]
        [RegularExpression(@"[a-z]{1}[0-9]{3}[a-z]{2}_[0-9]{2,3}", ErrorMessage = "Номер машины введён некорректно")]
        public string CarNumber { get; set; }
    }
}
