namespace AgendaPark_Backend.dtos
{
     public class dtoUsuario
    {
        public int id { get; set; }

        public string nome { get; set; }

        public string senha { get; set; }

        public string email { get; set; }

        public string nivel_acesso { get; set; }

        public string telefone { get; set; }

        public int numeroAvaliacoes { get; set; }

        public int numeroEstrelas { get; set; }
    }
}