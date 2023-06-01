using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
