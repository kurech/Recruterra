using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ResumeData
    {
        public User User { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<TypeOfEmployment> TypeOfEmployments { get; set; }
        public IFormFile ProfileImage { set; get; }
        public int ResumeId { set; get; }
        public string GetTypeOfEmployment(int id)
        {
            return TypeOfEmployments.FirstOrDefault(type => type.Id == id).Type;
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
