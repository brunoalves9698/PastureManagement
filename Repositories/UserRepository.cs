using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using PastureManagement.ViewModels.UserViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PastureManagement.Repositories
{
   public class UserRepository
   {
      private readonly DataContext _context;

      public UserRepository(DataContext context)
      {
         _context = context;
      }

      public ListUserViewModel GetById(int id)
      {
         var result = _context.Users.Find(id);
         if (result != null)
            return (ListUserViewModel)result;
         else
            return null;
      }

      public IEnumerable<ListUserViewModel> Get()
      {
         return _context.Users.ToList().Select(x => (ListUserViewModel)x);
      }

      public ListUserViewModel Authenticate(LoginViewModel user)
      {
         var result = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
         if (result != null)
            return (ListUserViewModel)result;
         else
            return null;
      }

      public bool ChangePassword(ChangePasswordViewModel changePasswordViewModel)
      {
         var result = _context.Users.Find(changePasswordViewModel.Id);
         if (result == null)
            return false;

         result.Password = changePasswordViewModel.Password;
         _context.Entry<User>(result).State = EntityState.Modified;
         _context.SaveChanges();

         return true;
      }

      public void Save(User user)
      {
         _context.Users.Add(user);
         _context.SaveChanges();
      }

      public void Update(User user)
      {
         _context.Entry<User>(user).State = EntityState.Modified;
         _context.Entry<User>(user).Property(x => x.Password).IsModified = false;
         _context.SaveChanges();
      }

      public void Delete(int id)
      {
         _context.Users.Remove(_context.Users.Find(id));
         _context.SaveChanges();
      }
   }
}
