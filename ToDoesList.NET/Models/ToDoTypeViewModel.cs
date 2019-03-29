using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoesList.NET.Models
{
    public class ToDoTypeViewModel
    {
        public List<ToDo> ToDoes;
        public SelectList Types;
        public string ToDoType { get; set; }
        public string SearchString { get; set; }
    }
}
