using AgendaPark_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using AgendaPark_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AgendaPark_Backend.Controllers
{
    [Route("api/equipamento")]
    [ApiController]
    public class EquipamentoController : Controller
    {
        private readonly DataContext _contexto;
        public EquipamentoController(DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Equipamento>>> pegarEquipamentoNaoDeletado()
        {
            return Ok(await _contexto.Equipamento.Where(x => x.deletado == false).ToArrayAsync());
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult<List<Equipamento>>> pegarEquipamento()
        {
            return Ok(await _contexto.Equipamento.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Equipamento>> BuscaPorId(int id)
        {

            return Ok(await _contexto.Equipamento.FindAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Equipamento>> salvarEquipamento (Equipamento dado)
        {
            _contexto.Equipamento.Add(dado);
            await _contexto.SaveChangesAsync();
            return Ok(dado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Equipamento>> editarEquipamento ([FromBody]Equipamento dado, int id)
        {
            var equipamento = await _contexto.Equipamento.FindAsync(id);
            if (equipamento == null)
                return BadRequest("Nada Encontrado");

            equipamento.nome = dado.nome;
            equipamento.tipo = dado.tipo;
            equipamento.marca = dado.marca;
            equipamento.modelo = dado.modelo;
            equipamento.patrimonio = dado.patrimonio;
            equipamento.localizacao = dado.localizacao;
            equipamento.status = dado.status;

            await _contexto.SaveChangesAsync();

            return Ok(equipamento);
        }

        [HttpDelete("logicamente/{id}")]
        [Authorize]
        public async Task<ActionResult<Equipamento>> DeletaEquipamentoLogicamente(int id)
        {
            var equipamento = await _contexto.Equipamento.FindAsync(id);
            if (equipamento == null)
                return BadRequest("Nada Encontrado");

            equipamento.deletado = true;

            await _contexto.SaveChangesAsync();

            return Ok(equipamento);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Equipamento>>> deletarEquipamento (int id)
        {
            var dado = await _contexto.Equipamento.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.Equipamento.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.Equipamento.ToListAsync());
        }
    }
}
