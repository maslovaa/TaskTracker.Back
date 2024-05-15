using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<ProjectEntity> ProjectEntities { get; set; }
        public DbSet<TaskEntity> TaskEntities { get; set; }
        public DbSet<DeskEntity> DeskEntities { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().ToTable("Projects");
            modelBuilder.Entity<ProjectEntity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<ProjectEntity>().HasMany(x => x.Desks).WithOne(x => x.Project);
            modelBuilder.Entity<ProjectEntity>().HasOne(x => x.Owner);

            modelBuilder.Entity<TaskEntity>().ToTable("Tasks");
            modelBuilder.Entity<TaskEntity>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<DeskEntity>().ToTable("Desks");
            modelBuilder.Entity<DeskEntity>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserEntity>()
                .HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
