using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class MeetingsData
    {
        public User User { get; set; }
        public Resume Resume { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<Employer> Employers { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
