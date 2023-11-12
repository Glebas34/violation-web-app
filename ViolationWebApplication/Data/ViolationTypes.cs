using System.ComponentModel.DataAnnotations;

namespace ViolationWebApplication.Data
{
    public enum ViolationTypes
    {
        [Display(Name = "Превышение скорости")]
        Speeding = 1,
        [Display(Name = "Пересечение двойной сплошной")]
        IntersectionOfADoublSolidMarkingLine = 2,
        [Display(Name = "Вождение в нетрезвом состоянии")]
        DrunkDriving = 3,
    }
}
