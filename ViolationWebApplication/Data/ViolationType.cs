using System.ComponentModel.DataAnnotations;

namespace ViolationWebApplication.Data
{
    public enum ViolationType
    {
        [Display(Name = "Превышение скорости")]
        Speeding,

        [Display(Name = "Пьяное вождение")]
        DrunkDriving,

        [Display(Name = "Пересечение двойной сплошной")]
        IntersectionOfASolidMarkingLine
    }
}
