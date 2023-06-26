using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Backend.Data;
using AgendaPark_Backend.Models;

namespace AgendaPark_Back.Controllers
{
    [Route("api/equipamentoAgenda")]
    [ApiController]
    public class EquipamentoAgendaController : Controller
    {
        private readonly DataContext _contexto;

        public EquipamentoAgendaController(DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<EquipamentoAgenda>>> pegarlaboratorioNaoDeletado()
        {
            return Ok(await _contexto.EquipamentoAgenda.ToArrayAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EquipamentoAgenda>> BuscaPorId(int id)
        {
            return Ok(await _contexto.EquipamentoAgenda.FindAsync(id));
        }

        [HttpGet("idAgenda/{id}")]
        [Authorize]
        public async Task<ActionResult<List<EquipamentoAgenda>>> BuscaPorIdAgenda(int id)
        {
            return Ok(await _contexto.EquipamentoAgenda.Where((x) => x.agendaid == id).Include(x => x.equipamento).ToArrayAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EquipamentoAgenda>> salvarlaboratorio (EquipamentoAgenda dado)
        {
            _contexto.EquipamentoAgenda.Add(dado);
            await _contexto.SaveChangesAsync();
            return Ok(dado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<EquipamentoAgenda>> editarEquipamento ([FromBody]EquipamentoAgenda dado, int id)
        {
            var eqAg = await _contexto.EquipamentoAgenda.FindAsync(id);
            if (eqAg == null)
                return BadRequest("Nada Encontrado");

            eqAg.agendaid = dado.agendaid;
            eqAg.equipamentoid = dado.equipamentoid;

            await _contexto.SaveChangesAsync();

            return Ok(eqAg);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<EquipamentoAgenda>>> deletarlaboratorio (int id)
        {
            var dado = await _contexto.EquipamentoAgenda.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.EquipamentoAgenda.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.EquipamentoAgenda.ToListAsync());
        }
    }
}