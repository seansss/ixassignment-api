using System;
using Microsoft.EntityFrameworkCore;

namespace Intelexual.Data
{
    // dotnet ef migrations add Intelexual_init -c ProjectDbContext -o Migrations/Intelexual.Data.Migrations --project Intelexual.API
    // dotnet ef migrations add Tables -c ProjectDbContext -o Migrations/Intelexual.Data.Migrations --project Intelexual.API
    public class ProjectDbContext : DbContext
    {
        internal string connection { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
            // Database.EnsureCreated();
            Database.Migrate();
        }

        public ProjectDbContext(string _connection)
        {
            this.connection = _connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(this.connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // See data annotations [Key] for defining PK on other tables.
            modelBuilder.Entity<Models.ProjectUser>(entity =>
            {
                entity.ToTable("ProjectUsers");
                entity.HasKey(e => e.Id);
            });
        }

        public DbSet<Models.Project> Projects { get; set; }
        public DbSet<Models.IXFile> IXFiles { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.ProjectUser> ProjectUsers { get; set; }
    }
}

