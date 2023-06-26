namespace AgendaPark_Backend.dtos
{
    public class dtoEquipamentoAgenda
    {
        public DateTime dataHoraChegada { get; set; }

        public DateTime dataHoraSaida { get; set; }

        public string atividade { get; set; }

        public string empresaCurso { get; set; }

        public string instituicaoCurso { get; set; }

        public int idLaboratorio { get; set; }

        public string responsavel { get; set; }

        public string observacao { get; set; }

        public int [] idEquipamentos { get; set; }

        public bool aprovado { get; set; }

        public bool deletado { get; set; }
    }
}