using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class OneOfTheVacancyData
    {
        public Vacancy Vacancy { get; set; }
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
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
                    return $"{Vacancy.WorkExperience} год";
                case 2:
                    return $"{Vacancy.WorkExperience} года";
                case 3:
                    return $"{Vacancy.WorkExperience} года";
                case 4:
                    return $"{Vacancy.WorkExperience} года";
                default:
                    return $"{Vacancy.WorkExperience} лет";
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
