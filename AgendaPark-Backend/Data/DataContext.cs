using AgendaPark_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaPark_Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {}

            
        public DbSet<Agenda> Agenda { get; set; }

        public DbSet<CaracteristicasLab> CaracteristicasLab { get; set; }

        public DbSet<Equipamento> Equipamento { get; set; }

        public DbSet<EquipamentoAgenda> EquipamentoAgenda { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Predio> Predio { get; set; }

        public DbSet<Laboratorio> Laboratorio { get; set; }

        public DbSet<Informacoes> Informacoes { get; set; }
    }
}