using System.ComponentModel.DataAnnotations;

namespace ViolationWebApplication.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string DriversLicense { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
