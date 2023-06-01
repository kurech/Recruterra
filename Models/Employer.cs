using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Postcode { get; set; }
        public int? IdCity { get; set; }
        public string? Street { get; set; }
        public string? House { get; set; }
        public string? Apartment { get; set; }
        public string? MSRN { get; set; }
        public bool? IsApproved { get; set; }

        [ForeignKey("Id")]
        public virtual User User { get; set; }

        [ForeignKey("IdCity")]
        public virtual City City { get; set; }

        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
