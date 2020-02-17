using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastureManagement.Models
{
   [Table("Animal")]
   public class Animal
   {
      [Key]
      public int Id { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
      [Display(Name = "Nome")]
      public string Name { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
      [Display(Name = "Raça")]
      public string Breed { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [Display(Name = "Peso")]
      public decimal Weight { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [ForeignKey("HerdCategory")]
      [Display(Name = "Categoria Rebanho")]
      public int Id_HerdCategory { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      [ForeignKey("Pasture")]
      [Display(Name = "Pasto")]
      public int Id_Pasture { get; set; }

      public HerdCategory HerdCategory { get; set; }
      public Pasture Pasture { get; set; }
   }
}
