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

        public IActionResult Dettagli(int id)
        {
            
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? singlePizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (singlePizza == null)
                {
                    return NotFound($"La pizza con {id} non è stata trovata!");
                }
                else
                {
                    return View("Dettagli", singlePizza);
                }
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
