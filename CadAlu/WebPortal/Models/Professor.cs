using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.Models
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Telefone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int escola { get; set; }
        [Required]
        public string Disciplina { get; set; }
    }
}
