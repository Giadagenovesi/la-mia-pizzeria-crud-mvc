using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("Index", pizzas);
            }
        }

        public IActionResult UserIndex()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("UserIndex", pizzas);
            }
        }
    }
}
