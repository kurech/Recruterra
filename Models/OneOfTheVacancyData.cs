using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Models
{
    public class OneOfTheVacancyData
    {
        public Vacancy Vacancy { get; set; }
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public IEnumerable<TypeOfEmployment> TypeOfEmployments { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Citizenship> Citizenships { get; set; }
        public IEnumerable<Employer> Employers { get; set; }

        public string GetWorkEx()
        {
            switch (Vacancy.WorkExperience)
            {
                case 0:
                    return "Без опыта";
                case 1:
                    return $"от {Vacancy.WorkExperience} года";
                case 2:
                    return $"от {Vacancy.WorkExperience} лет";
                case 3:
                    return $"от {Vacancy.WorkExperience} лет";
                case 4:
                    return $"от {Vacancy.WorkExperience} лет";
                case 5:
                    return $"от {Vacancy.WorkExperience} лет";
                case 6:
                    return $"от {Vacancy.WorkExperience} лет";
                default:
                    return $"от {Vacancy.WorkExperience} лет";
            }
        }

        public string GetTypeOfEmployment()
        {
            return TypeOfEmployments.FirstOrDefault(type => type.Id == Vacancy.Id).Type;
        }

        public string GetCity()
        {
            return Cities.FirstOrDefault(type => type.Id == Vacancy.Id).Name;
        }

        public string GetCitizenship()
        {
            return Citizenships.FirstOrDefault(type => type.Id == Vacancy.Id).Name;
        }
    }
}
