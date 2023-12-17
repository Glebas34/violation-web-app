using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViolationWebApplication.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string? CarNumber { get; set; }
        public string? Manufacturer {  get; set; }
        public string? Model { get; set; }
        [ForeignKey(nameof(Owner))]
        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }
        public ICollection<Violation>? Violations;
    }
}
