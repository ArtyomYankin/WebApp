using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebApp.Controllers
{
    
    public class HomeController : Controller
    {
        
        private readonly UserContext _context;

        public HomeController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {   
                List<UserViewModel> vm = new List<UserViewModel>();
                var items = _context.Users.ToList();
                foreach (var item in items)
                {
                    vm.Add(new UserViewModel
                    {
                        Id = item.Id,
                        Email = item.Email,
                        IsBlocked = item.IsBlocked
                    });
                }
                return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(List<UserViewModel> emp)
        {
            List<User> userToDelete = new List<User>();
            foreach (var item in emp)
            {
                if (item.Emps.Selected)
                {
                    var selectedUser = _context.Users.Find(item.Id);
                    userToDelete.Add(selectedUser);
                }

            }
            _context.Users.RemoveRange(userToDelete);
            _context.SaveChanges();
            if(!_context.Users.Where(x => x.Email == User.Identity.Name).Any())
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Block(List<UserViewModel> emp)
        {
            List<User> userToBlock = new List<User>();
            foreach (var item in emp)
            {
                if (item.Emps.Selected)
                {
                   
                    var selectedUser = _context.Users.Find(item.Id);
                    _context.Users.Where(e =>e.Id == item.Id).FirstOrDefault().IsBlocked = true;
                    item.IsBlocked = true;
                    _context.Users.Update(_context.Users.Find(item.Id));
                }
            }
            if (_context.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault()?.IsBlocked == true)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UnBlock(List<UserViewModel> emp)
        {
            List<User> userToBlock = new List<User>();
            foreach (var item in emp)
            {
                if (item.Emps.Selected)
                {
                   
                    var selectedUser = _context.Users.Find(item.Id);
                    _context.Users.Where(e => e.Id == item.Id).FirstOrDefault().IsBlocked = false;
                    item.IsBlocked = false;
                    _context.Users.Update(_context.Users.Find(item.Id));
                }

            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

