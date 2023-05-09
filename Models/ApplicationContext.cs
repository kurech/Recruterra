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
        public DbSet<Article> Articles { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<TypeOfEmployment> TypeOfEmployments { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacancy>()
                .HasOne(p => p.Employer)
                .WithMany(t => t.Vacancies)
                .HasForeignKey(x => x.IdEmployer)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Meeting>()
                .HasOne(p => p.Resume)
                .WithMany(t => t.Meetings)
                .HasForeignKey(x => x.IdResume)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        }
    }
}
