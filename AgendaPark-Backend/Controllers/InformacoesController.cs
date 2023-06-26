using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Backend.Data;
using AgendaPark_Backend.Models;

namespace AgendaPark_Back.Controllers
{
    [Route("api/informacoes")]
    [ApiController]
    public class InformacoesController : Controller
    {
        private readonly DataContext _contexto;

        public InformacoesController (DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Informacoes>>> pegarInformacoesNaoDeletado()
        {
            return Ok(await _contexto.Informacoes.Where(x => x.deletado == false).ToArrayAsync());
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult<List<Informacoes>>> Get()
        {

            return Ok(await _contexto.Informacoes.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Informacoes>> BuscaPorId(int id)
        {

            return Ok(await _contexto.Informacoes.FindAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Informacoes>> SalvarInformacoes (Informacoes dado)
        {
            _contexto.Informacoes.Add(dado);
            await _contexto.SaveChangesAsync();
            return Ok(dado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Informacoes>> EditaInformacoes ([FromBody]Informacoes dado, int id)
        {
            var Informacoes = await _contexto.Informacoes.FindAsync(id);
            if (Informacoes == null)
                return BadRequest("Nada Encontrado");

            Informacoes.url = dado.url;
            Informacoes.titulo = dado.titulo;
            Informacoes.descricao = dado.descricao;

            await _contexto.SaveChangesAsync();

            return Ok(Informacoes);
        }

        [HttpDelete("logicamente/{id}")]
        [Authorize]
        public async Task<ActionResult<Informacoes>> DeletaInformacoesLogicamente(int id)
        {
            var Informacoes = await _contexto.Informacoes.FindAsync(id);
            if (Informacoes == null)
                return BadRequest("Nada Encontrado");

            Informacoes.deletado = true;

            await _contexto.SaveChangesAsync();

            return Ok(Informacoes);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Informacoes>>> Delete(int id)
        {
            var dado = await _contexto.Informacoes.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.Informacoes.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.Informacoes.ToListAsync());
        }

    }
}