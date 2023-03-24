using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
