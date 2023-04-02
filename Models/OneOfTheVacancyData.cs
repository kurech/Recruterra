using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class OneOfTheVacancyData
    {
        public Vacancy Vacancy { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
