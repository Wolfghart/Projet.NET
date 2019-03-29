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

        public async Task<IActionResult> Modify( int ID)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var todo = Context.ToDo.First(t => t.Id == ID && t.User==user);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Modify( int id, ToDo todo)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var oldtodo = Context.ToDo.First(t => t.Id == id && t.User==user);
            if (oldtodo == null)
            {
                return NotFound();
            }
            oldtodo.Title = todo.Title;
            oldtodo.Description = todo.Description;
            oldtodo.Date = todo.Date;
            Context.ToDo.Update(oldtodo);
            Context.SaveChanges();
            return RedirectToAction("Index","ToDo");
        }

        public async Task<IActionResult> delete(int ID)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var todo = Context.ToDo.First(t => t.Id == ID && t.User == user);
            if (todo == null)
            {
                return NotFound();
            }
            Context.ToDo.Remove(todo);
            Context.SaveChanges();

            return RedirectToAction("Index","ToDo");
        }
    }
}