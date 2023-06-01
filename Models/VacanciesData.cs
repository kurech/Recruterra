using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class VacanciesData
    {
        public IEnumerable<Vacancy> Vacancies { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
