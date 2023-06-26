using AgendaPark_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using AgendaPark_Backend.Models;
using AgendaPark_Backend.Services;
using AgendaPark_Backend.dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Back.Services;

namespace AgendaPark_Back.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly DataContext _contexto;

        public UsuarioController(DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> pegarUsuarioNaoDeletado()
        {
            return Ok(await _contexto.Usuario.Where(x => x.deletado == false).ToArrayAsync());
        }

        [HttpGet("naoAprovados")]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> retornaNaoAprovados()
        {
            return Ok(await _contexto.Usuario.Where(x => x.aprovado == false && x.deletado == false).ToArrayAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> BuscaPorId(int id)
        {

            return Ok(await _contexto.Usuario.FindAsync(id));
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> BuscaTodosUsuarios()
        {

            return Ok(await UsuarioService.BuscaTodosUsuarios(_contexto));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> AddDadoUsuario(dtoUsuario dado)
        {
            Usuario usuario = new Usuario();

            usuario.id = 0;
            usuario.nome = dado.nome;
            usuario.email = dado.email;
            usuario.senha = dado.senha;
            usuario.nivel_acesso = dado.nivel_acesso;
            usuario.telefone = dado.telefone;
            usuario.numeroAvaliacoes = dado.numeroAvaliacoes;
            usuario.numeroEstrelas = dado.numeroEstrelas;
            usuario.deletado = false;
            usuario.aprovado = false;

            _contexto.Usuario.Add(usuario);
            await _contexto.SaveChangesAsync();

            Email emailDestinatario = new Email();

            emailDestinatario.assunto = "Solicitação de Acesso";
            emailDestinatario.corpo = "Parabéns, você fez uma solicitação de acesso, por favor, aguarde até uma laboratorista aceitar! Te avisaremos por aqui.";
            emailDestinatario.destinatario = usuario.email;

            await EmailService.mandaEmail(emailDestinatario);

            List<Usuario> laboratoristas = await _contexto.Usuario.Where((x) => x.nivel_acesso == "laboratorista").ToListAsync();

            foreach (Usuario labs in laboratoristas)
            {
                Email emailLaboratorista = new Email();

                emailLaboratorista.assunto = "Solicitação de Acesso";
                emailLaboratorista.corpo = "Olá, " + usuario.nome + " solicitou um acesso ao sistema atravéz do email " + usuario.email + " e esta esperando você respondelo pelo sistema!";
                emailLaboratorista.destinatario = labs.email;

                await EmailService.mandaEmail(emailLaboratorista);
            }

            return Ok(dado);
        }

        [HttpPost("aceitaUsuario/{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> aceitaUsuario(int id)
        {
            Usuario usuario = await _contexto.Usuario.FindAsync(id);
            if (usuario == null)
                return BadRequest("Nada Encontrado");

            usuario.aprovado = true;


            Email emailDestinatario = new Email();

            emailDestinatario.assunto = "Solicitação de Acesso Aprovada";
            emailDestinatario.corpo = "Parabéns, " + usuario.nome + " sua solicitação de acesso foi aceita. Seu acesso está liberado com as credenciais cadastradas!.";
            emailDestinatario.destinatario = usuario.email;

            await EmailService.mandaEmail(emailDestinatario);

            await _contexto.SaveChangesAsync();
            return Ok("Aprovado Com Sucesso.");
        }

        [HttpPost("recusa/{id}/{motivo}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> recusaUsuario(int id, string motivo)
        {
            var usuario = await _contexto.Usuario.FindAsync(id);
            if (usuario == null)
                return BadRequest("Nada Encontrado");

            usuario.deletado = true;

            Email emailDestinatario = new Email();

            try
            {
                emailDestinatario.assunto = "Solicitação de Acesso Negada";
                emailDestinatario.corpo = usuario.nome + ", infelizmente sua solicitação de acesso foi negada. Motivo:" + motivo;
                emailDestinatario.destinatario = usuario.email;

                await EmailService.mandaEmail(emailDestinatario);
            }
            catch (Exception ex)
            {
                await _contexto.SaveChangesAsync();
                return BadRequest("Email não valido, mas usuário recusa com sucesso.");
            }

            return Ok("Negado Com Sucesso.");
        }

        [HttpPost("authenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] dtoUsuarioSenha dado)
        {
            var resposta = await UsuarioService.Login(dado);

            if (resposta == null)
                return NotFound("Usuario ou senha invalidos");

            return Ok(resposta);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> EditaDadoUsuario([FromBody] Usuario dado, int id)
        {
            var usuario = await _contexto.Usuario.FindAsync(id);
            if (usuario == null)
                return BadRequest("Nada Encontrado");

            usuario.nome = dado.nome;
            usuario.email = dado.email;
            usuario.senha = dado.senha;
            usuario.nivel_acesso = dado.nivel_acesso;
            usuario.telefone = dado.telefone;
            usuario.aprovado = dado.aprovado;
            usuario.numeroAvaliacoes = dado.numeroAvaliacoes;
            usuario.numeroEstrelas = dado.numeroEstrelas;

            await _contexto.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpPut("alteraNivelAcesso/{id}/{nivelAcesso}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> EditaNivelAcesso(string nivelAcesso, int id)
        {
            var usuario = await _contexto.Usuario.FindAsync(id);
            if (usuario == null)
                return BadRequest("Nada Encontrado");

            usuario.nivel_acesso = nivelAcesso;

            await _contexto.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpDelete("logicamente/{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> DeletaUsuarioLogicamente(int id)
        {
            var usuario = await _contexto.Usuario.FindAsync(id);
            if (usuario == null)
                return BadRequest("Nada Encontrado");

            usuario.deletado = true;

            await _contexto.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> Delete(int id)
        {
            var dado = await _contexto.Usuario.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.Usuario.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.Usuario.ToListAsync());
        }
    }
}
