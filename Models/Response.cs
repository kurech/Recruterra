using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? IsAccepted { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        [ForeignKey("IdVacancy")]
        public virtual Vacancy Vacancy { get; set; }
    }
}
