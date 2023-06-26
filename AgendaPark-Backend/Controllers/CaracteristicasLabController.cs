using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Backend.Data;
using AgendaPark_Backend.Models;

namespace AgendaPark_Back.Controllers
{
    [Route("api/CaracteristicasLab")]
    [ApiController]
    public class CaracteristicasLabController : Controller
    {
        private readonly DataContext _contexto;

        public CaracteristicasLabController(DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CaracteristicasLab>>> pegarlaboratorioNaoDeletado()
        {
            return Ok(await _contexto.CaracteristicasLab.ToArrayAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CaracteristicasLab>> BuscaPorId(int id)
        {

            return Ok(await _contexto.CaracteristicasLab.FindAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CaracteristicasLab>> salvarlaboratorio (CaracteristicasLab dado)
        {
            _contexto.CaracteristicasLab.Add(dado);
            await _contexto.SaveChangesAsync();
            return Ok(dado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<CaracteristicasLab>> editarCaracteristicaLaboratorio ([FromBody]CaracteristicasLab dado, int id)
        {
            var eqAg = await _contexto.CaracteristicasLab.FindAsync(id);
            if (eqAg == null)
                return BadRequest("Nada Encontrado");

            eqAg.laboratorioid = dado.laboratorioid;
            eqAg.nome = dado.nome;
            eqAg.valor = dado.valor;

            await _contexto.SaveChangesAsync();

            return Ok(eqAg);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<CaracteristicasLab>>> deletarlaboratorio (int id)
        {
            var dado = await _contexto.CaracteristicasLab.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.CaracteristicasLab.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.CaracteristicasLab.ToListAsync());
        }
    }
}