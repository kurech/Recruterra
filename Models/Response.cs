using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Response
    {
        public int Id { get; set; } 
        public int IdUser { get; set; }
        public int IdVacancy { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}
