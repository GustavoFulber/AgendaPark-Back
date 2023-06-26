namespace AgendaPark_Backend.Repositories
{
    public class ConexaoBanco
    {
        public static string conexaoBanco() 
        {
            return "server=localhost ;port=3306;database=agendaPark;user=root;password=root;Persist Security Info=False; Connect Timeout=300";
        }
    }
}
