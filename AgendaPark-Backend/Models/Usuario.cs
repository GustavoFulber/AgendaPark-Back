using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaPark_Backend.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string nome { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string email { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string senha { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string nivel_acesso { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string telefone { get; set; }

        public int numeroAvaliacoes { get; set; }

        public int numeroEstrelas { get; set; }

        public bool aprovado { get; set; }

        public bool deletado { get; set; }
    }
}