using Microsoft.EntityFrameworkCore;
using PastureManagement.Models;

namespace PastureManagement.Data
{
   public class DataContext : DbContext
   {
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer(@"Server=TROJANEXE\SQLEXPRESS;DataBase=PastureManagement;Trusted_Connection=true;");
      }

      public DbSet<User> Users { get; set; }

      public DbSet<HerdCategory> HerdCategories { get; set; }

      public DbSet<Animal> Animals { get; set; }

      public DbSet<Pasture> Pastures { get; set; }
   }
}