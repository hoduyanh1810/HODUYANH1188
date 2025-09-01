using Microsoft.AspNetCore.Mvc;
using BTH.Models;

namespace BTH.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Person person)
        {
            if (ModelState.IsValid)
            {
                return View("Result", person);
            }
            return View();
        }
    }
}
