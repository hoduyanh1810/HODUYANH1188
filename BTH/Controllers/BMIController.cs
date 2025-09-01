using Microsoft.AspNetCore.Mvc;
using BTH.Models;

namespace BTH.Controllers
{
    public class BMIController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BMI model)
        {
            if (ModelState.IsValid)
            {
                model.Calculate();
                return View("Result", model);
            }
            return View();
        }
    }
}
