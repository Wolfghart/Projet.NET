using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoesList.NET.Models;

namespace ToDoesList.NET.Data
{
    public class ToDoContext : IdentityDbContext<User>
    {
        public ToDoContext (DbContextOptions<ToDoContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoesList.NET.Models.ToDo> ToDo { get; set; }
    }
}
