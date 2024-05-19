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
        public DbSet<RoleEntity> RolesEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().ToTable("Projects");
            modelBuilder.Entity<ProjectEntity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(x => x.Users);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(x => x.Tasks);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(x => x.Desks);

            modelBuilder.Entity<TaskEntity>().ToTable("Tasks");
            modelBuilder.Entity<TaskEntity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<TaskEntity>()
                .HasOne(x => x.Project);
            modelBuilder.Entity<TaskEntity>()
                .HasOne(x => x.Desk);

            modelBuilder.Entity<DeskEntity>().ToTable("Desks");
            modelBuilder.Entity<DeskEntity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<DeskEntity>()
                .HasMany(x => x.Tasks);
            modelBuilder.Entity<DeskEntity>()
                .HasOne(x => x.Project);

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserEntity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.Projects);
            modelBuilder.Entity<UserEntity>()
                .HasOne(x => x.Role)
                .WithOne(x => x.User)
                .HasForeignKey<RoleEntity>(x => x.Id);


            modelBuilder.Entity<RoleEntity>().ToTable("Roles");
            modelBuilder.Entity<RoleEntity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<RoleEntity>()
                .HasOne(x => x.User)
                .WithOne(x => x.Role)
                .HasForeignKey<UserEntity>(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
