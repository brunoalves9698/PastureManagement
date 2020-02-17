using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace PastureManagement.Repositories
{
   public class AnimalRepository
   {
      private readonly DataContext _context;

      public AnimalRepository(DataContext context)
      {
         _context = context;
      }

      public Animal GetById(int id)
      {
         return _context.Animals
            .Include(x => x.HerdCategory)
            .Include(x => x.Pasture)
            .SingleOrDefault(x => x.Id == id);
      }

      public IEnumerable<Animal> Get()
      {
         return _context.Animals
            .Include(x => x.HerdCategory)
            .Include(x => x.Pasture)
            .ToList();
      }

      public void Save(Animal animal)
      {
         _context.Animals.Add(animal);
         _context.SaveChanges();
      }

      public void Update(Animal animal)
      {
         _context.Entry<Animal>(animal).State = EntityState.Modified;
         _context.SaveChanges();
      }

      public void Delete(int id)
      {
         _context.Animals.Remove(_context.Animals.Find(id));
         _context.SaveChanges();
      }
   }
}
