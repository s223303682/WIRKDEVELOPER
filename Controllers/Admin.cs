using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;

namespace WIRKDEVELOPER.Controllers
{
	public class Admin : Controller
	{
        private readonly ApplicationDBContext _Context;

        public Admin(ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult AdminDashboard()
		{
			return View();
		}

        public IActionResult AddActiveIngredient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddActiveIngredient(ActiveIngredient activeIngredient)
		{
            if (ModelState.IsValid)
            {
                _Context.activeIngredients.Add(activeIngredient);
                _Context.SaveChanges();
                return RedirectToAction("ActiveIngredientsList");
            }
            return View(activeIngredient);
        }
        public IActionResult ActiveIngredientsList()
        {
            IEnumerable<ActiveIngredient> list = _Context.activeIngredients;
            return View(list);
        }

    }
}
