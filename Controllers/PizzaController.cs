using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public static ListaDellePizze pizze = null;
        public IActionResult Index()
        {
            if (pizze == null)
            {
                Pizza Margherita = new Pizza(0, "Margherita", "Ingredienti: Mozzarella, Pomodoro, Basilico", 5.00, "img/margherita.jpg");
                Pizza Boscaiola = new Pizza(1, "Boscaiola", "Ingredienti: Mozzarella, Salsiccia, Funghi", 7.00, "img/boscaiola.jpg");
                Pizza Wustel = new Pizza(2, "Bufala", "Ingredienti: Wustel, Mozzarella,", 6.50, "img/wustel.jpg");
             

                pizze = new();
                pizze.pizza.Add(Margherita);
                pizze.pizza.Add(Boscaiola);
                pizze.pizza.Add(Wustel);
            }


            return View(pizze);
        }


        public IActionResult Show(int id)
        {
            return View("Show", pizze.pizza[id]);
        }

        public IActionResult CreaNuovaPizza()
        {
            Pizza NuovaPizza = new Pizza()
            {
                Id = 0,
                Name = "",
                Description = "",
                Price = 0.0,
                Photo = "",

            };

            return View(NuovaPizza);
        }


        public IActionResult ShowPizza(Pizza pizza)
        {

            if (!ModelState.IsValid)
            {
                return View("CreaNuovaPizza", pizza);
            }

            Pizza nuovaPizza = new Pizza()
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                Price = pizza.Price,
                Photo = pizza.Photo,

            };
            pizze.pizza.Add(nuovaPizza);
            return View("ShowPizza", nuovaPizza);
        }


        public IActionResult AggiornaPizza(Pizza pizza)
        {

            return View("AggiornaPizza", pizza);
        }

        public IActionResult EditPizza(Pizza pizza)
        {
            //Pizza updatePizza = new Pizza();
            //updatePizza = (Pizza)pizze.pizzas.Where(x => x.Id == pizza.Id);

            Pizza updatePizza = pizze.pizza.Find(x => x.Id == pizza.Id);

            updatePizza.Name = pizza.Name;
            updatePizza.Description = pizza.Description;
            updatePizza.Price = pizza.Price;
            if (updatePizza.Photo != pizza.Photo)
            {
                updatePizza.Photo = pizza.Photo;
            }



            return View("Show", updatePizza);
        }




        public IActionResult Rimuovi(Pizza pizza)
        {

            return View("Rimuovi", pizza);
        }



        [HttpPost]
        public IActionResult Delete(Pizza pizza)
        {
            Pizza updatePizza = pizze.pizza.Find(x => x.Id == pizza.Id);
            if (updatePizza.Id == pizza.Id)
            {
                var ok = pizze.pizza.Remove(updatePizza);
                Console.WriteLine(ok);
            }
            return RedirectToAction("Index");
        }
    }
}