using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public int IdCity { get; set; }
        public int WorkExperience { get; set; }
        public string Description { get; set; }
        public string Education { get; set; }
        public int IdTypeOfEmployment { get; set; }
        public int IdUser { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

        [ForeignKey("IdCity")]
        public virtual City City { get; set; }

        [ForeignKey("IdTypeOfEmployment")]
        public virtual TypeOfEmployment TypeOfEmployment { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
