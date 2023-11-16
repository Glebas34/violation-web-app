using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Service
{
    public class ViolationService : IViolationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ViolationService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteViolation(Violation violation)
        {
            violation = await _unitOfWork.ViolationRepository.Get(violation.Id);
            await _unitOfWork.ViolationRepository.ExplicitLoading(violation, "Car");
            await _unitOfWork.ViolationRepository.Delete(violation.Id);
            _unitOfWork.Complete();

            List<Violation> violations = (await _unitOfWork.ViolationRepository.GetAll()).Where(e => e.CarId == violation.CarId).ToList();
            if (violations.Count == 0)
            {
                await _unitOfWork.CarRepository.Delete(violation.Car.Id);
                _unitOfWork.Complete();
            }
        }
    }
}
