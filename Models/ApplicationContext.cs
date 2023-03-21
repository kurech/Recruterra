using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<TypeOfEmployment> TypeOfEmployments { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
