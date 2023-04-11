using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ResponsesData
    {
        public User User { get; set; }
        public Resume resume { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Vacancy> Vacancies { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
