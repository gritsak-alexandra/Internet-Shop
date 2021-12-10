using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using shhop.Models;
using shhop.ViewModels;

namespace shhop.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name) //создание роли
        {
            if (!string.IsNullOrEmpty(name))// название роли не пустое
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name)); //создание новой роли
                if (result.Succeeded) //если успешно
                {
                    return RedirectToAction("Index");
                }
                else //иначе
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) //удаление роли
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id); //получаем id пользователя
            if (role != null) //если существует
            {
                IdentityResult result = await _roleManager.DeleteAsync(role); //удаляем роль
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList()); //

        public async Task<IActionResult> Edit(string userId) //вывод ролей конкретного пользователя
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel //вывод модели
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model); // вовращаем модель
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles) // редактирование прав доступа пользователям
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles); // добавление роли пользователю

                await _userManager.RemoveFromRolesAsync(user, removedRoles); //удаление роли пользователя

                return RedirectToAction("UserList"); // возвращение на список пользователей
            }

            return NotFound();
        }
    }
}
