using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoesList.NET.Data;
using ToDoesList.NET.Models;

namespace ToDoesList.NET.Controllers
{
    public class ToDoController : Controller
    {
        public ToDoContext Context { get; set; }
        public UserManager<User> UserManager { get; set; }
        private Task<User> GetCurrentUserAsync() => UserManager.GetUserAsync(HttpContext.User);

        public ToDoController(ToDoContext ctx, UserManager<User> UM)
        {
            this.Context = ctx;
            this.UserManager = UM;

        }

        public async Task<IActionResult> Create(ToDo todo)
        {

            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                todo.UserId = user.Id;
                todo.User = user;
                Context.ToDo.Add(todo);
                Context.SaveChanges();
                return Redirect("Index");
            }
            return View("Index", Context.ToDo.ToList());
            
        }

        public async Task<IActionResult> Index()
        {
            User usr = await GetCurrentUserAsync();
            if(usr==null)
            {
                return View("ErreurConnexion");
            }
            var liste = Context.ToDo.Where(m => m.User == usr).ToList<ToDo>();

            if (liste == null)
            {
                liste = new List<ToDo>();
            }
            return View(liste);
        }
    }
}