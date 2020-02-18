using Microsoft.EntityFrameworkCore;
using PastureManagement.Models;

namespace PastureManagement.Data
{
   public class DataContext : DbContext
   {
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer(@"YOUR_CONNECTION_STRING");
      }

      public DbSet<User> Users { get; set; }

      public DbSet<HerdCategory> HerdCategories { get; set; }

      public DbSet<Animal> Animals { get; set; }

      public DbSet<Pasture> Pastures { get; set; }
   }
}