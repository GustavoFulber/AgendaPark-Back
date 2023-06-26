namespace AgendaPark_Backend.dtos
{
    public class dtoAgenda
    {
        public int id { get; set; }

        public DateTime dataHoraChegada { get; set; }

        public DateTime dataHoraSaida { get; set; }

        public string atividade { get; set; }

        public string empresaCurso { get; set; }

        public string instituicaoCurso { get; set; }

        public int laboratorioid { get; set; }

        public string responsavel { get; set; }

        public string observacao { get; set; }

        public bool aprovado { get; set; }

        public bool deletado { get; set; }
    }
}