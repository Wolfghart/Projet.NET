using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoesList.NET.Models
{
    public class User: IdentityUser
    {
        public ICollection<ToDo> Todoes { get; set; }
    }
}
