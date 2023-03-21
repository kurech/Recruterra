using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class TypeOfEmployment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
