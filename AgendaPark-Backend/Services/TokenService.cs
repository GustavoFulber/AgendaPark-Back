using AgendaPark_Backend.dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Backend_agenda_park;

namespace AgendaPark_Backend.Services
{
    public static class TokenService
    {
        public static string GerarToken (dtoUsuarioSemSenha user)
        {
            var token = new JwtSecurityTokenHandler();

            var senha = Encoding.ASCII.GetBytes(Settings.jwtSecret);

            var tokenDescript = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Role, user.nivel_acesso),
                }),

                Expires = DateTime.UtcNow.AddHours(14),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(senha), SecurityAlgorithms.HmacSha256Signature)
            };   

            var tokenCriado = token.CreateToken(tokenDescript);
            return token.WriteToken(tokenCriado);
        }
    }
}
