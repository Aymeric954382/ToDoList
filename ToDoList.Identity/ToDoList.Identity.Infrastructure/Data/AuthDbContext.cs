using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ToDoList.Identity.Domain.Entities;
using ToDoList.Identity.Infrastructure.Configuration.EntityTypeConfiguration;

namespace ToDoList.Identity.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
                entity.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(entity =>
                entity.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
                entity.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
                entity.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
                entity.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
                entity.ToTable("UserTokens"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
                entity.ToTable("RoleClaims"));

            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }

    }
}
