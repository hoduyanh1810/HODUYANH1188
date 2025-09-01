using Microsoft.AspNetCore.Mvc;
using BTH.Models;

namespace BTH.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Calculator model)
        {
            switch (model.Operation)
            {
                case "add":
                    model.Result = model.Number1 + model.Number2;
                    break;
                case "sub":
                    model.Result = model.Number1 - model.Number2;
                    break;
                case "mul":
                    model.Result = model.Number1 * model.Number2;
                    break;
                case "div":
                    model.Result = model.Number2 != 0 ? model.Number1 / model.Number2 : double.NaN;
                    break;
            }
            return View("Result", model);
        }
    }
}
