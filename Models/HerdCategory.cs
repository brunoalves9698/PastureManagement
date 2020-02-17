using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastureManagement.Models
{
   [Table("HerdCategory")]
   public class HerdCategory
   {
      [Key]
      public int Id { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
      [Display(Name = "Nome")]
      public string Name { get; set; }
   }
}
