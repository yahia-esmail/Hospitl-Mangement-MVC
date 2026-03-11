using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospitl_Mangement_MVC.Controllers
{
    [Authorize(Roles = "Nurse")]
    public class NurseController : Controller
    {
        public NurseController(IGenericRepository<Medication> repository)
        {
            _repository = repository;
        }
        private readonly IGenericRepository<Medication> _repository;
        public IActionResult GetAllMedicines()
        {
            return View(_repository.GetAll());
        }
        public IActionResult AddMedicine()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedicine(Medication medication)
        {
            _repository.Add(medication);
            return RedirectToAction("GetAllMedicines");
        }
        public IActionResult GetMedicine(int id)
        {
            return View(_repository.GetById(id));
        }
        public IActionResult UpdateMedicine(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var medicine = _repository.GetById(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);

        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMedicine(Medication medication)
        {
            _repository.Update(medication);
            return RedirectToAction("GetAllMedicines");
        }
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var medicine = _repository.GetById(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMedicine(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetAllMedicines");
        }
    }
}
