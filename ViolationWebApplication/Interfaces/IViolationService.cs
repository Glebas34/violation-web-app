using ViolationWebApplication.Models;

namespace ViolationWebApplication.Interfaces
{
    public interface IViolationService
    {
        public Task ChangeStatus(Violation violation);
    }
}
