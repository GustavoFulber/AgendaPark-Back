using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Backend.Data;
using AgendaPark_Backend.Models;
using AgendaPark_Backend.dtos;

namespace AgendaPark_Back.Controllers
{
    [Route("api/laboratorio")]
    [ApiController]
    public class LaboratorioController : Controller
    {
        private readonly DataContext _contexto;

        public LaboratorioController(DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Laboratorio>>> pegarlaboratorioNaoDeletado()
        {
            return Ok(await _contexto.Laboratorio.Where(x => x.deletado == false).ToArrayAsync());
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult<List<Laboratorio>>> pegarlaboratorio()
        {
            return Ok(await _contexto.Laboratorio.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Laboratorio>> BuscaPorId(int id)
        {

            return Ok(await _contexto.Laboratorio.FindAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Laboratorio>> salvarlaboratorio(dtoLaboratorio dado)
        {
            Predio predio = await _contexto.Predio.FindAsync(dado.predioid);
            if (predio == null)
                return BadRequest("Nada Encontrado");

            Laboratorio lab = new Laboratorio();

            lab.nome = dado.nome;
            lab.sigla = dado.sigla;
            lab.m2 = dado.m2;
            lab.capacidade = dado.capacidade;
            lab.predioid = dado.predioid;
            lab.localiza_dentro_Predio = dado.localiza_dentro_Predio;
            lab.deletado = false;
            lab.predio = predio;
            
            _contexto.Laboratorio.Add(lab);

            await _contexto.SaveChangesAsync();

            return Ok(dado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Laboratorio>> editarlaboratorio([FromBody]Laboratorio dado, int id)
        {
            var laboratorio = await _contexto.Laboratorio.FindAsync(id);
            if (laboratorio == null)
                return BadRequest("Nada Encontrado");

            laboratorio.nome = dado.nome;
            laboratorio.sigla = dado.sigla;
            laboratorio.m2 = dado.m2;
            laboratorio.capacidade = dado.capacidade;
            laboratorio.predioid = dado.predioid;
            laboratorio.localiza_dentro_Predio = dado.localiza_dentro_Predio;

            await _contexto.SaveChangesAsync();

            return Ok(laboratorio);
        }

        [HttpDelete("logicamente/{id}")]
        [Authorize]
        public async Task<ActionResult<Laboratorio>> DeletalaboratorioLogicamente(int id)
        {
            var laboratorio = await _contexto.Laboratorio.FindAsync(id);
            if (laboratorio == null)
                return BadRequest("Nada Encontrado");

            laboratorio.deletado = true;

            await _contexto.SaveChangesAsync();

            return Ok(laboratorio);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Laboratorio>>> deletarlaboratorio(int id)
        {
            var dado = await _contexto.Laboratorio.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.Laboratorio.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.Laboratorio.ToListAsync());
        }
    }
}