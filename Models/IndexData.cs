using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class IndexData
    {
        public User User { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<Vacancy> Vacancies { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
