namespace AgendaPark_Backend.dtos
{
    public class dtoUsuarioSenha
    {

        public string email { get; set; }

        public string senha { get; set; }

        public dtoUsuarioSenha(string email, string senha)
        {
            this.email = email;
            this.senha = senha;
        }
    }
}