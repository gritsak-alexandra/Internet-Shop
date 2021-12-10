using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using shhop.Models;
using shhop.ViewModels;

namespace shhop.Controllers 
{
    public class UsersController : Controller
    {
    UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index() => View(_userManager.Users.ToList());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel model) //создание пользователя
    {
        if (ModelState.IsValid)
        {
            User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year }; //получаем модель и создаем пользователя
            var result = await _userManager.CreateAsync(user, model.Password); //получаем результат создания
            if (result.Succeeded) //если успешно
            {
                return RedirectToAction("Index"); //возвращаем результат
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(string id) //вывод инфы о пользователе
    {
        User user = await _userManager.FindByIdAsync(id); //получаем пользователя
        if (user == null) //если не сущетсвует
        {
            return NotFound();
        }
        EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year }; //вывод информации
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel model) //изменение информации о пользователе
    {
        if (ModelState.IsValid) 
        {
            User user = await _userManager.FindByIdAsync(model.Id); //получаем модель пользователя по id
            if (user != null) //если пользователь существует
            {
                user.Email = model.Email; //устанавливаем значения
                user.UserName = model.Email;
                user.Year = model.Year;

                var result = await _userManager.UpdateAsync(user); //получаем результат изменнеий 
                if (result.Succeeded) //если успешно
                {
                    return RedirectToAction("Index"); //выводим  результат
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id) //удаление пользователя
    {
        User user = await _userManager.FindByIdAsync(id); //получаем id пользователя
        if (user != null) //если существует
        {
            IdentityResult result = await _userManager.DeleteAsync(user); //удаление пользователя
        }
        return RedirectToAction("Index");
    }
}
}
