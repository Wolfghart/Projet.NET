using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace ToDoesList.NET.Models
{
    [Authorize]
    public class ToDo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        

    }
}
