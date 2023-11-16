using ViolationWebApplication.Models;

namespace ViolationWebApplication.Interfaces
{
    public interface IViolationService
    {
        public Task DeleteViolation(Violation violation);
    }
}
