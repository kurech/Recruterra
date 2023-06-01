using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class MeetingsData
    {
        public User User { get; set; }
        public Resume Resume { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<Employer> Employers { get; set; }
    }
}
