using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Identity.Domain.Entities;
using ToDoList.Identity.Infrastructure.Configuration.EntityTypeConfiguration;

namespace ToDoList.Identity.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
                entity.ToTable("Users"));
            modelBuilder.Entity<IdentityRole<Guid>>(entity =>
                entity.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
                entity.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
                entity.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
                entity.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
                entity.ToTable("UserTokens"));
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
                entity.ToTable("RoleClaims"));

            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }
    }
}
