using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GCDealershipCapstone.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
//using AspNetCore;

namespace GCDealershipCapstone.Controllers
{
    public class HomeController : Controller
    {
        private InventoryAPIDAL IAPI = new InventoryAPIDAL();
        private readonly GCDealershipIdentityContext _context;

        public HomeController(GCDealershipIdentityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
                
        public async Task<IActionResult> ViewInventory()
        {
            var inventory = await IAPI.GetInventory();
            return View(inventory);
        }
        
        public async Task<IActionResult> SearchInventory(string make, string model, string style, int year, string color, int mileage)
        {
            if (make == null)
            {
                make = "0";
            }
            if (model == null)
            {
                model = "0";
            }
            if (style == null)
            {
                style = "0";
            }
            if (color == null)
            {
                color = "0";
            }

            var results = await IAPI.GetInventorySearch(make, model, style, year, color, mileage);
            return View(results);            
        }
        [Authorize]
        public IActionResult AddToFavorite(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<UserFavorite> favorites = _context.UserFavorite.ToList();
            UserFavorite newFav = new UserFavorite();

            newFav.CarId = id;
            newFav.UserId = userId;
            if (ModelState.IsValid)
            {
                _context.UserFavorite.Add(newFav);
                _context.SaveChanges();
            }
            return RedirectToAction("Favorites");
        }
        
        public async Task<IActionResult> Favorites()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<UserFavorite> favoriteCarIds = _context.UserFavorite.Where(x => x.UserId == userId).ToList();
            List<Inventory> favoriteCars = new List<Inventory>();
            foreach (UserFavorite f in favoriteCarIds)
            {
                Inventory car = await IAPI.GetCar((int)f.CarId);
                favoriteCars.Add(car);
            }

            return View(favoriteCars);
        }

        public IActionResult RemoveFromFavorite(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<UserFavorite> favoriteToRemove =  _context.UserFavorite.Where(x => x.UserId == userId &&
                                                                           x.CarId == id).ToList();

            _context.UserFavorite.Remove(favoriteToRemove[0]);
            _context.SaveChanges();
            return RedirectToAction("Favorites");
        }

    }
}
