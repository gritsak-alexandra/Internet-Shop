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

        public IActionResult GetCatHud()// вывод товаров в категорию худ.литературы
        {
            return RedirectToAction("Index", "Home", new { category = "4" });
        }
        public IActionResult GetCatMang() // вывод товаров в категорию манги
        {
            return RedirectToAction("Index", "Home", new { category = "2" });
        }

        public async Task<IActionResult> AddToCart(int prodId) //добавление в корзину
        {
            var user = await _userManager.GetUserAsync(User); //получение пользователя в системе

            await _applicationContext.Cart.AddAsync(new Cart() { UserId = user.Id, ThingId = prodId }); // создание корзины и добавление в нее товара для пользователя
            await _applicationContext.SaveChangesAsync(); //сохранение изменений

            return RedirectToAction("Index", "Home"); //возвращение на главную страницу
        }

        public async Task<IActionResult> ShowCart() //вывод содержимого корзины
        {
            var user = await _userManager.GetUserAsync(User); //получение пользователя в системе

            var tempcart = _applicationContext.Cart //получение содержимого корзины для пользователя
                .Where(c => c.UserId == user.Id) //для пользователя
                .Select(c => c.Thing) //каждый товар
                .ToList();
            var cart = new Dictionary<Thing, int>(); // словарь из товара и их количества
            foreach(var thing in tempcart) //цикл по товарам
            {
                int count = tempcart.Count(t => t == thing); //подсчет товаров
                cart.TryAdd(thing, count); //добавление товара в словарь
            }
            return View(cart);
        }
        public async Task<IActionResult> Confirm() //оформление заказа
        {
            var user = await _userManager.GetUserAsync(User); //получение пользователя в системе

            var cart = _applicationContext.Cart //получение содержимого корзины для пользователя
                .Where(c => c.UserId == user.Id); //

            _applicationContext.RemoveRange(cart); //оформление заказа и очищение корзины
            await _applicationContext.SaveChangesAsync() //сохранение изменений
;
            return View();
        }
    }
}
