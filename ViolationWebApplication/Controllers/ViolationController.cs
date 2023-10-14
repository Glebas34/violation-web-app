using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.ViewModels;

namespace ViolationWebApplication.Controllers
{
    [Route("Violation")]
    public class ViolationController : Controller
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public ViolationController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
        }
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> Add(ViewModelViolation model) {
            Car? car = await _unitOfWork.CarRepository.GetByNumber(model.CarNumber);
            Violation violation = new Violation();
            violation.TypeOfViolation = model.TypeOfViolation;
            violation.FineFee = model.FineFee;
            if (car != null)
            {
                violation.Car = car;
                violation.CarId = car.Id;
                await _unitOfWork.ViolationRepository.Add(violation);
                _unitOfWork.Complete();
                return Redirect("Home/Index/");
            }
            car = new Car();
            car.CarNumber = model.CarNumber;
            violation.Car = car;
            await _unitOfWork.CarRepository.Add(car);
            _unitOfWork.Complete();
            await _unitOfWork.ViolationRepository.Add(violation);
            _unitOfWork.Complete();
            return Redirect("Car/Create");
        }
    }
}
