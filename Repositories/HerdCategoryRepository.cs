using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace PastureManagement.Repositories
{
   public class HerdCategoryRepository
   {
      private readonly DataContext _context;

      public HerdCategoryRepository(DataContext context)
      {
         _context = context;
      }

      public HerdCategory GetById(int id)
      {
         return _context.HerdCategories.Find(id);
      }

      public IEnumerable<HerdCategory> Get()
      {
         return _context.HerdCategories.ToList();
      }

      public void Save(HerdCategory category)
      {
         _context.HerdCategories.Add(category);
         _context.SaveChanges();
      }

      public void Update(HerdCategory category)
      {
         _context.Entry<HerdCategory>(category).State = EntityState.Modified;
         _context.SaveChanges();
      }

      public void Delete(int id)
      {
         _context.HerdCategories.Remove(_context.HerdCategories.Find(id));
         _context.SaveChanges();
      }
   }
}
