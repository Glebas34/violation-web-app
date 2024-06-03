using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViolationWebApplication.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string Manufacturer {  get; set; }
        public string Model { get; set; }
        public string OwnerPassport { get; set; }
        public string OwnerFullName { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Violation> Violations;
    }
}
