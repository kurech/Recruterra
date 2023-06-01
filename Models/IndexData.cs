using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Models
{
    public class IndexData
    {
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<Vacancy> Vacancies { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Response> Responses { get; set; }
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
                case 21:
                    return "года";
                case 22:
                    return "года";
                case 23:
                    return "года";
                case 24:
                    return "года";
                case 31:
                    return "года";
                case 32:
                    return "года";
                case 33:
                    return "года";
                case 34:
                    return "года";
                case 41:
                    return "года";
                case 42:
                    return "года";
                case 43:
                    return "года";
                case 44:
                    return "года";
                case 51:
                    return "года";
                case 52:
                    return "года";
                case 53:
                    return "года";
                case 54:
                    return "года";
                case 61:
                    return "года";
                case 62:
                    return "года";
                case 63:
                    return "года";
                case 64:
                    return "года";
                case 71:
                    return "года";
                case 72:
                    return "года";
                case 73:
                    return "года";
                case 74:
                    return "года";
                default:
                    return "лет";
            }
        }
    }
}
