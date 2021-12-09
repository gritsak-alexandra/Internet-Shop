using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shhop.Models;
using Microsoft.AspNetCore.Identity;

namespace shhop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<User> _userManager;

        public ShopController(ApplicationContext applicationContext, UserManager<User> userManager)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        public IActionResult GetCatHud()
        {
            return RedirectToAction("Index", "Home", new { category = "4" });
        }
        public IActionResult GetCatMang()
        {
            return RedirectToAction("Index", "Home", new { category = "2" });
        }

        public async Task<IActionResult> AddToCart(int prodId)
        {
            var user = await _userManager.GetUserAsync(User);

            await _applicationContext.Cart.AddAsync(new Cart() { UserId = user.Id, ThingId = prodId });
            await _applicationContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ShowCart()
        {
            var user = await _userManager.GetUserAsync(User);

            var tempcart = _applicationContext.Cart
                .Where(c => c.UserId == user.Id)
                .Select(c => c.Thing)
                .ToList();
            var cart = new Dictionary<Thing, int>();
            foreach(var thing in tempcart)
            {
                int count = tempcart.Count(t => t == thing);
                cart.TryAdd(thing, count);
            }
            return View(cart);
        }
        public async Task<IActionResult> Confirm()
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = _applicationContext.Cart
                .Where(c => c.UserId == user.Id);

            _applicationContext.RemoveRange(cart);
            await _applicationContext.SaveChangesAsync()
;
            return View();
        }
    }
}
