using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaPark_Backend.Models
{
    public class CaracteristicasLab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string nome { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string valor { get; set; }

        public int laboratorioid { get; set; }

        public Laboratorio laboratorio { get; set; }
    }
}