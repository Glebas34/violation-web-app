using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViolationWebApplication.Data;

namespace ViolationWebApplication.Models
{
    public class Violation
    {
        [Key]
        public int Id { get; set; }
        public ViolationType TypeOfViolation { get; set; }
        public int FineFee { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; }=false;
        [ForeignKey(nameof(Car))]
        public int? CarId { get; set; }
        public Car? Car { get; set; }
    }
}
