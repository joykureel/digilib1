using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DigiLib.Models;

namespace DigiLib.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Subject>().ToTable("Subject");
            builder.Entity<Contact>().ToTable("Contact");
            builder.Entity<Book>().ToTable("Book");
            builder.Entity<Video>().ToTable("Video");
            builder.Entity<Url>().ToTable("Url");
            builder.Entity<Presentation>().ToTable("Presentation");
            builder.Entity<Quiz>().ToTable("Quiz");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<DigiLib.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
