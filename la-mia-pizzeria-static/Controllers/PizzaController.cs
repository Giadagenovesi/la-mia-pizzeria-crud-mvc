using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private ICustomLogger _myLogger;
        private PizzeriaContext _myDatabase;

        public PizzaController(PizzeriaContext db, ICustomLogger logger)
        {
            _myLogger = logger;
            _myDatabase = db;
        }
        public IActionResult Index()
        {
            _myLogger.WriteLog("Pagina Admin gestione Pizze");
            List<Pizza> pizzas = _myDatabase.Pizzas.Include(Pizza => Pizza.Category).ToList<Pizza>();

            return View("Index", pizzas);
        }


        //READ
        public IActionResult Dettagli(int id)
        {  
            Pizza? singlePizza = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).Include(Pizza => Pizza.Category).FirstOrDefault();

            if (singlePizza == null)
            {
               return View("NotFoundPage");
            }
            else
            {
               return View("Dettagli", singlePizza);
            }
        }

        public IActionResult UserIndex()
        {
            _myLogger.WriteLog("Pagina Home Pizze");
            List<Pizza> pizzas = _myDatabase.Pizzas.Include(Pizza => Pizza.Category).ToList<Pizza>();

            return View("UserIndex", pizzas);

        }


        //CREATE
        [HttpGet]
        public IActionResult Aggiungi()
        {
            List<Category> categories = _myDatabase.Categories.ToList();

            PizzaFormModel model =
                new PizzaFormModel { Pizza = new Pizza(), Categories = categories };
            return View("Aggiungi", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Aggiungi(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;
                return View("Aggiungi", data);
            }

            _myDatabase.Pizzas.Add(data.Pizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");  
        }

        //UPDATE

        [HttpGet]
        public IActionResult Aggiorna(int id)
        {
            Pizza? pizzaToChange = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToChange == null)
            {
                return View("NotFoundPage");
            }
            else
            {
                List<Category> categories = _myDatabase.Categories.ToList();

                PizzaFormModel model
                    = new PizzaFormModel { Pizza = pizzaToChange, Categories = categories };

                return View("Aggiorna", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Aggiorna(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;
                return View("Aggiorna", data); ;
            }

            // Variante più complessa e astratta, ma alla lunga più immediata
            Pizza? pizzaToUpdate = _myDatabase.Pizzas.Find(id);

            if (pizzaToUpdate != null)
            {
                EntityEntry<Pizza> entryEntity = _myDatabase.Entry(pizzaToUpdate);
                entryEntity.CurrentValues.SetValues(data.Pizza);

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("NotFoundPage");
            }

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
        }
        //DELETE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancella(int id)
        {
            Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToDelete != null)
            {
                _myDatabase.Pizzas.Remove(pizzaToDelete);
                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("NotFoundPage");
            }
        }
    }
}
