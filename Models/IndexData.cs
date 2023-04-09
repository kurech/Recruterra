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
        public IEnumerable<TypeOfEmployment> TypeOfEmployments { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Citizenship> Citizenships { get; set; }
        public IEnumerable<Employer> Employers { get; set; }
        public string GetTypeOfEmployment(int id)
        {
            return TypeOfEmployments.FirstOrDefault(type => type.Id == id).Type;
        }
        public string GetCity(int id)
        {
            return Cities.FirstOrDefault(type => type.Id == id).Name;
        }
        public string GetCitizenship(int id)
        {
            return Citizenships.FirstOrDefault(type => type.Id == id).Name;
        }
        public string GetYearNameInRussian(int? year)
        {
            switch (year)
            {
                case 0:
                    return "Без опыта";
                case 1:
                    return "год";
                case 2:
                    return "года";
                case 3:
                    return "года";
                case 4:
                    return "года";
                default:
                    return "лет";
            }
        }
    }
}
