using System.ComponentModel.DataAnnotations;

namespace PastureManagement.Models
{
   public class User
   {
      public int Id { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
      [Display(Name = "Usuário")]
      public string UserName { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
      [Display(Name = "Senha")]
      public string Password { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
      [Display(Name = "Perfil")]
      public string Role { get; set; }
   }
}