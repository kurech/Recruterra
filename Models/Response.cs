using System;
using System.ComponentModel.DataAnnotations.Schema;

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
