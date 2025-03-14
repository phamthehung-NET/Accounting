using Accounting.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Data;

public class IdentityDbContext : IdentityDbContext<CustomUser>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedUser(builder);
        SeedRoleAndUserRole(builder);
    }

    private static void SeedUser(ModelBuilder builder)
    {
        var passwordHasher = new PasswordHasher<CustomUser>();

        CustomUser admin = new()
        {
            Id = "1",
            Email = "admin@gmail.com",
            UserName = "admin",
            //NormalizedEmail = "admin@gmail.com",
            NormalizedUserName = "admin",
            PasswordHash = passwordHasher.HashPassword(null, "123456"),
            EmailConfirmed = true,
            LockoutEnabled = true,
            UpdatedPriceDate = DateTime.UtcNow,
        };
        builder.Entity<CustomUser>().HasData(admin);
    }

    private static void SeedRoleAndUserRole(ModelBuilder builder)
    {
        IdentityRole role = new()
        {
            Id = "1",
            Name = "Admin",
            NormalizedName = "admin",
        };
        builder.Entity<IdentityRole>().HasData(role);
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>() { RoleId = "1", UserId = "1" });
    }
}
