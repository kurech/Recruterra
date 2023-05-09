using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public string? Photo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public int? IdCity { get; set; }
        public int? IdCitizenship { get; set; }
        public string? Position { get; set; }
        public int? Salary { get; set; }
        public string? Education { get; set; }
        public string? University { get; set; }
        public int? WorkExperience { get; set; }
        public int? IdTypeOfEmployment { get; set; }
        public string? AdditionalInformation { get; set; }
        public bool? ItsPublic { get; set; }

        [ForeignKey("Id")]
        public virtual User User { get; set; }

        [ForeignKey("IdCitizenship")]
        public virtual Citizenship Citizenship { get; set; }

        [ForeignKey("IdCity")]
        public virtual City City { get; set; }

        [ForeignKey("IdTypeOfEmployment")]
        public virtual TypeOfEmployment TypeOfEmployment { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
