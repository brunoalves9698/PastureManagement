using PastureManagement.Models;

namespace PastureManagement.ViewModels.UserViewModels
{
   public class ListUserViewModel
   {
      public int Id { get; set; }
      public string UserName { get; set; }

      public string Role { get; set; }

      public static explicit operator ListUserViewModel(User user)
      {
         return new ListUserViewModel
         {
            Id = user.Id,
            UserName = user.UserName,
            Role = user.Role
         };
      }
   }
}
