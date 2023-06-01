using System.Collections.Generic;

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
