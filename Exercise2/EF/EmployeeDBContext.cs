using EmployeeManagerment.Configuration;
using EmployeeManagerment.Entity;
using Exercise2.Configuration;
using Exercise2.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exercise2.EF
{
    public class EmployeeDBContext : IdentityDbContext<AppUser,AppRole, int>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole").HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin").HasKey(x => x.UserId);
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken").HasKey(x => x.UserId);
            builder.Seed();
           
        }
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
