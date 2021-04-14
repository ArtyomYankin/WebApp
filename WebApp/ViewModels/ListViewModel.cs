using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ListViewModel
    {
       public int Id { get; set; }
        public string Email { get; set; }
        public SelectListItem Emps { get; set; }
    }
}
