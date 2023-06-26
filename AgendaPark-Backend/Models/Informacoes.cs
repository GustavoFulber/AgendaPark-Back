using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaPark_Backend.Models
{
    public class Informacoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string url { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string titulo { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string descricao { get; set; }

        public bool deletado { get; set; }
    }
}