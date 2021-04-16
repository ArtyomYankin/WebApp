using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class UserViewModel
    { 
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public SelectListItem Emps { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArticleDate { get; set; }
    }
}
