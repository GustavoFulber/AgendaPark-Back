using AgendaPark_Backend.Models;
using AgendaPark_Backend.dtos;
using AgendaPark_Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using AgendaPark_Backend.Data;

namespace AgendaPark_Backend.Services
{
    public class UsuarioService
    {
        private readonly DataContext _contexto;

        public UsuarioService(DataContext context)
        {
            _contexto = context;
        }

        public static async Task<ActionResult<dynamic>?> Login(dtoUsuarioSenha dado)
        {
            var connection = new MySqlConnection(ConexaoBanco.conexaoBanco());
            await connection.OpenAsync();

            using var command = new MySqlCommand(UsuarioRepository.Login(dado.email, dado.senha), connection);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                dtoUsuarioSemSenha usuario = new dtoUsuarioSemSenha(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7));

                var token = TokenService.GerarToken(usuario);
                return new
                {
                    usuario = usuario,
                    token = "Bearer " + token,
                    expiracao = DateTime.UtcNow.AddHours(21)
                };
            }

            return null;
        }

        public static async Task<ActionResult<List<Usuario>>> BuscaTodosUsuarios(DataContext _context)
        {
            return await _context.Usuario.ToListAsync();
        }
    }
}
