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
        public async Task ChangeStatus(Violation violation)
        {
            await _unitOfWork.ViolationRepository.ExplicitLoading(violation, "Car");
            violation.IsPaid = true;
            _unitOfWork.ViolationRepository.Update(violation);

            List<Violation> violations = (await _unitOfWork.ViolationRepository.GetAll()).Where(e => e.IsPaid == false && e.CarId == violation.CarId ).ToList();
            if (violations.Count == 0) 
            {
                violations = (await _unitOfWork.ViolationRepository.GetAll()).Where(e => e.CarId == violation.CarId).ToList();
                _unitOfWork.ViolationRepository.DeleteRange(violations);

                Car car = violation.Car;
                await _unitOfWork.CarRepository.Delete(car.Id);
                await _unitOfWork.CarRepository.ExplicitLoading(car, "Owner");

                Owner owner = car.Owner;
                await _unitOfWork.OwnerRepository.ExplicitLoading(owner, "Cars");

                List<Car> cars = (owner.Cars).ToList();
                if (cars.Count == 0)
                {
                    await _unitOfWork.OwnerRepository.Delete(owner.Id);
                }
                _unitOfWork.Complete();
            }
        }
    }
}
