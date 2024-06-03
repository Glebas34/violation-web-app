using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViolationWebApplication.Data;

namespace ViolationWebApplication.Models
{
    public class Violation
    {
        [Key]
        public int Id { get; init; }
        public ViolationType TypeOfViolation { get; set; }
        public int FineFee { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [ForeignKey(nameof(Car))]
        public int? CarId { get; set; }
        public Car? Car { get; set; }
    }
}
