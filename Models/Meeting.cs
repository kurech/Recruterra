using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public bool IsActive { get; set; }
        public int IdResume { get; set; }
        public int IdEmployer { get; set; }

        [ForeignKey("IdResume")]
        public virtual Resume Resume { get; set; }

        [ForeignKey("IdEmployer")]
        public virtual Employer Employer { get; set; }
    }
}
