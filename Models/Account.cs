using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int Auth { get; set; }
        public DateTime CurentDateTime { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
