using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaPark_Backend.Models
{
    public class Laboratorio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string nome { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string sigla { get; set; }

        public float m2 { get; set; }

        public int capacidade { get; set; }
        
        public int predioid { get; set; }

        [MaxLength(255, ErrorMessage = "{0} deve ter no maximo {1} caractares.")]
        public string localiza_dentro_Predio { get; set; }

        public bool deletado { get; set; }

        public Predio predio { get; set; }
    }
}