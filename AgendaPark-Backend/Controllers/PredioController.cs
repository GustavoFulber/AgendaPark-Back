using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Backend.Data;
using AgendaPark_Backend.Models;

namespace AgendaPark_Back.Controllers
{
    [Route("api/predio")]
    [ApiController]
    public class PredioController : Controller
    {
        private readonly DataContext _contexto;

        public PredioController (DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Predio>>> pegarPredioNaoDeletado()
        {
            return Ok(await _contexto.Predio.Where(x => x.deletado == false).ToArrayAsync());
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult<List<Predio>>> Get()
        {

            return Ok(await _contexto.Predio.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Predio>> BuscaPorId(int id)
        {

            return Ok(await _contexto.Predio.FindAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Predio>> SalvarPredio (Predio dado)
        {
            _contexto.Predio.Add(dado);
            await _contexto.SaveChangesAsync();
            return Ok(dado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Predio>> EditaPredio ([FromBody]Predio dado, int id)
        {
            var Predio = await _contexto.Predio.FindAsync(id);
            if (Predio == null)
                return BadRequest("Nada Encontrado");

            Predio.nome = dado.nome;
            Predio.endereco = dado.endereco;

            await _contexto.SaveChangesAsync();

            return Ok(Predio);
        }

        [HttpDelete("logicamente/{id}")]
        [Authorize]
        public async Task<ActionResult<Predio>> DeletaPredioLogicamente(int id)
        {
            var predio = await _contexto.Predio.FindAsync(id);
            if (predio == null)
                return BadRequest("Nada Encontrado");

            predio.deletado = true;

            await _contexto.SaveChangesAsync();

            return Ok(predio);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Predio>>> Delete(int id)
        {
            var dado = await _contexto.Predio.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.Predio.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.Predio.ToListAsync());
        }

    }
}