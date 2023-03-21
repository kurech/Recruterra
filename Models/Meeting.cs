using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
