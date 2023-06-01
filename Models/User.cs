using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
