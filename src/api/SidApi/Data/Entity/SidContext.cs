using Microsoft.EntityFrameworkCore;
using SidApi.Data.Entities;
 
namespace SidApi.Data;
 
public class SidContext : DbContext
{
    //Constructor
    public SidContext(DbContextOptions<SidContext> options): base(options)
    {
        
    }
 
    public DbSet<User> Users {get;set;}
    public DbSet<Login> Logins {get;set;}
    public DbSet<SchoolInfo> SchoolInfos {get;set;}
    public DbSet<Store> Stores {get;set;}
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            //Config constraints
            modelBuilder.Entity<Login>()
            .HasIndex(l => l.Username)
            .IsUnique();
 
            modelBuilder.Entity<Login>()
            .HasOne(l => l.User).WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}