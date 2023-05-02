using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthAPI.Context;
public class UserContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _conString;
    public UserContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _conString = _configuration.GetConnectionString("DefaultConnection");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_conString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.ContactNumber);
            entity.Property(e => e.Password);
            modelBuilder.Entity<User>().ToTable("users");
        });

        modelBuilder.Entity<Role>(entity =>
       {
           entity.HasKey(e => e.RoleId);
           entity.Property(e => e.RoleName);
           modelBuilder.Entity<Role>().ToTable("roles");
       });

        modelBuilder.Entity<UserRole>(entity =>
       {
        entity.HasKey(e => e.UserRoleId);
        entity.Property(e => e.UserId);
        entity.Property(e => e.RoleId);
        modelBuilder.Entity<UserRole>().ToTable("user_roles");
       });
    }
}
