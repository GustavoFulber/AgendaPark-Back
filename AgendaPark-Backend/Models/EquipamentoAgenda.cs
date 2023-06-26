using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaPark_Backend.Models
{
    public class EquipamentoAgenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int equipamentoid { get; set; }

        public int agendaid { get; set; }

        public Equipamento equipamento { get; set; }

        public Agenda agenda { get; set; }
    }
}