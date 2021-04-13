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
        public HomeController(UserContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var list = new List<ListViewModel>();
            var users = await db.Users.ToListAsync();
            foreach (var u in users)
            {
                list.Add(new ListViewModel
                {
                    User = u,
                    Flag = false
                });
            }
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

