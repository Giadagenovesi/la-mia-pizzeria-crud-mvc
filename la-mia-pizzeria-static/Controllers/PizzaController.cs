using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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


        //READ
        public IActionResult Dettagli(int id)
        {
            
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? singlePizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (singlePizza == null)
                {
                    return View("NotFoundPage");
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


        //CREATE
        [HttpGet]
        public IActionResult Aggiungi()
        {
            return View("Aggiungi");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Aggiungi(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Aggiungi", newPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizzas.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        //UPDATE

        [HttpGet]
        public IActionResult Aggiorna(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaToChange = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToChange == null)
                {
                    return View("NotFoundPage");
                }
                else
                {
                    return View("Aggiorna", pizzaToChange);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Aggiorna(int id, Pizza changedPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Aggiorna", changedPizza);
            }

            using (PizzeriaContext db =new PizzeriaContext())
            {

                // Variante più semplice 

                /*
                Pizza? pizzaToUpdate = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizzaToUpdate != null)
                {
                    pizzaToUpdate.Gusto = changedPizza.Gusto;
                    pizzaToUpdate.Ingredienti = changedPizza.Ingredienti;
                    pizzaToUpdate.Prezzo = changedPizza.Prezzo;
                    pizzaToUpdate.Immagine = changedPizza.Immagine;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                } else
                {
                    return return View("NotFoundPage");
                }*/


                // Variante più complessa e astratta, ma alla lunga più immediata
                Pizza? pizzaToUpdate = db.Pizzas.Find(id);

                if (pizzaToUpdate != null)
                {
                    EntityEntry<Pizza>entryEntity = db.Entry(pizzaToUpdate);
                    entryEntity.CurrentValues.SetValues(changedPizza);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("NotFoundPage");
                }
            }
        }


        //DELETE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancella(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaToDelete = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("NotFoundPage");
                }
            }
        }

    }
}
