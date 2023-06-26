namespace AgendaPark_Backend.dtos
{
    public class dtoUsuarioSemSenha
    {
        public int id { get; set; }

        public string nome { get; set; }

        public string email { get; set; }

        public string nivel_acesso { get; set; }

        public string telefone { get; set; }

        public int numeroAvaliacoes { get; set; }

        public int numeroEstrelas { get; set; }

        public dtoUsuarioSemSenha(int id, string nome, string email, string nivel_acesso, string telefone, int numeroAvaliacoes, int numeroEstrelas)
        {
            this.id = id;

            this.nome = nome;

            this.email = email;

            this.nivel_acesso = nivel_acesso;

            this.telefone = telefone;

            this.numeroAvaliacoes = numeroAvaliacoes;

            this.numeroEstrelas = numeroEstrelas;
        }
    }
}