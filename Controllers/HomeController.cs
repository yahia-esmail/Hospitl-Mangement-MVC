using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospitl_Mangement_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Department> _department;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Department> department)
        {
            _logger = logger;
            _department = department;
        }


        public IActionResult Index()
        {
            var departments = _department.GetAll();//select * from department >>> pass to view
            return View(departments);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

