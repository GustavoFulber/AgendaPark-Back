using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaPark_Backend.Models
{
    public class Equipamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string nome { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string tipo { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string marca { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string modelo { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string patrimonio { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string localizacao { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string status { get; set; }

        public bool deletado { get; set; }
    }
}