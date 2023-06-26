namespace AgendaPark_Backend.Models
{
    public class dtoPredio
    {
        public string nome { get; set; }

        public string endereco { get; set; }
       
        public bool deletado { get; set; }

        public dtoPredio( string nome, string endereco, bool deletado)
        {
            this.nome = nome;
            this.endereco = endereco;
            this.deletado = deletado;
        }
    }
}