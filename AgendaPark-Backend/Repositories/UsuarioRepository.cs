namespace AgendaPark_Backend.Repositories
{
   public class UsuarioRepository
    {
        public static string Login(string email,string senha)
        {
            return "SELECT * FROM `agendaPark`.Usuario where email = " + '"' + email + '"' + " AND senha = " + '"' + senha + '"' + ";";
        }
    }
}
