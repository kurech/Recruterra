using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int IdCity { get; set; }
        public int IdCitizenship { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string Education { get; set; }
        public int WorkExperience { get; set; }
        public int IdTypeOfEmployment { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
