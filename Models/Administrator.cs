using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Administrator
    {
        [Key]
        public int Id { get; set; }
        public int TelegramId { get; set; }

        [ForeignKey("Id")]
        public virtual User User { get; set; }
    }
}
