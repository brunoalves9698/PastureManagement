using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace PastureManagement.Repositories
{
   public class PastureRepository
   {
      private readonly DataContext _context;

      public PastureRepository(DataContext context)
      {
         _context = context;
      }

      public Pasture GetById(int id)
      {
         return _context.Pastures.Find(id);
      }

      public IEnumerable<Pasture> Get()
      {
         return _context.Pastures.ToList();
      }

      public void Save(Pasture pasture)
      {
         _context.Pastures.Add(pasture);
         _context.SaveChanges();
      }

      public void Update(Pasture pasture)
      {
         _context.Entry<Pasture>(pasture).State = EntityState.Modified;
         _context.SaveChanges();
      }

      public void Delete(int id)
      {
         _context.Pastures.Remove(_context.Pastures.Find(id));
         _context.SaveChanges();
      }
   }
}
