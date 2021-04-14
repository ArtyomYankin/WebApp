using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
      
        private UserContext db;

        private readonly UserContext _context;

        public HomeController(UserContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {   
                var vm = new List<ListViewModel>();
                var items = await _context.Users.ToListAsync();
                foreach (var item in items)
                {
                    vm.Add(new ListViewModel
                    {
                        Id = item.Id,
                        Email = item.Email
                    });
                }
                return View(vm);
        }
        [HttpPost]
        public IActionResult DeleteUser(List<ListViewModel> emp)
        {
            List<User> user = new List<User>();
            foreach (var item in emp)
            {
                if (item.Emps.Selected)
                {
                    var selectedUser = _context.Users.Find(item.Id);
                    user.Add(selectedUser);
                }

            }
            _context.Users.RemoveRange(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}

