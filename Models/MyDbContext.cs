using Microsoft.EntityFrameworkCore;

namespace SigortaApp.Models
{
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<User> Users { get; set; }
}

public class Vehicle
{
    public int Id { get; set; }
    public string Plate { get; set; }  
    public string Make { get; set; }
    public string Model { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Insurance
{
    public int Id { get; set; }  
    public DateTime ExpirationAt { get; set; }
    public string CompanyName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class User
{
    public int Id { get; set; }  
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}
}
