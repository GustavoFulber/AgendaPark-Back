using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AgendaPark_Backend.Data;
using AgendaPark_Backend.Models;
using AgendaPark_Backend.dtos;
using AgendaPark_Back.Services;

namespace AgendaPark_Back.Controllers
{
    [Route("api/agenda")]
    [ApiController]
    public class AgendaController : Controller
    {
        private readonly DataContext _contexto;

        public AgendaController(DataContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<dynamic>>> pegarlaboratorioNaoDeletado()
        {
            return Ok(await _contexto.Agenda.Where(x => x.deletado == false).Include((x) => x.laboratorio).ToArrayAsync());
        }

        [HttpGet("aprovados")]
        [Authorize]
        public async Task<ActionResult<List<Agenda>>> pegarlaboratorioNaoDeletadoEAprovados()
        {
            return Ok(await _contexto.Agenda.Where(x => x.deletado == false && x.aprovado == true).Include((x) => x.laboratorio).ToArrayAsync());
        }

        [HttpGet("naoAprovados")]
        [Authorize]
        public async Task<ActionResult<List<Agenda>>> naoAprovados()
        {
            return Ok(await _contexto.Agenda.Where(x => x.deletado == false && x.aprovado == false).Include((x) => x.laboratorio).ToArrayAsync());
        }


        [HttpGet("DoDia")]
        [Authorize]
        public async Task<ActionResult<List<dynamic>>> pegarlaboratorioNaoDeletadoEAprovadosDodIA()
        {
            DateTime hoje = DateTime.Now.AddHours(-3);
            DateTime comecoDia = new DateTime(hoje.Year, hoje.Month, hoje.Day, 0, 0, 1);
            DateTime fimDia = new DateTime(hoje.Year, hoje.Month, hoje.Day, 23, 59, 59);
            return Ok(await _contexto.Agenda.Where(x => x.deletado == false && x.aprovado == true && x.dataHoraChegada > comecoDia && (x.dataHoraSaida < fimDia)).Include((x) => x.laboratorio).ToArrayAsync());
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<ActionResult<List<Agenda>>> pegarlaboratorio()
        {
            return Ok(await _contexto.Agenda.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Agenda>> BuscaPorId(int id)
        {

            return Ok(await _contexto.Agenda.FindAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<EquipamentoAgenda>>> salvarAgendamento(dtoEquipamentoAgenda dado)
        {
            Laboratorio laboratorio = await _contexto.Laboratorio.FindAsync(dado.idLaboratorio);
            if (laboratorio == null)
                return BadRequest("Nada Encontrado");

            Agenda agenda = new Agenda();

            dado.dataHoraChegada = dado.dataHoraChegada.AddHours(-3);

            dado.dataHoraSaida = dado.dataHoraSaida.AddHours(-3);

            if (dado.dataHoraChegada >= dado.dataHoraSaida)
            {
                return BadRequest("Data de Chegada não pode ser Maior ou Igual a de Saida!");
            }

            if (dado.dataHoraChegada < DateTime.Now.AddHours(-3))
            {
                return BadRequest("Data de chegada não pode ser anterior a agora!");
            }

            List<Agenda> agendas = await _contexto.Agenda.Where(x => x.laboratorioid == laboratorio.id && x.dataHoraChegada > DateTime.Now.AddHours(-3)
                && ((x.dataHoraChegada >= dado.dataHoraChegada && x.dataHoraChegada <= dado.dataHoraSaida) ||
                (x.dataHoraChegada <= dado.dataHoraSaida && x.dataHoraSaida >= dado.dataHoraChegada))).ToListAsync();

            if (agendas.Count > 0)
            {
                return BadRequest("Laboratorio já Agendado!");
            }

            foreach (int idEquipamento in dado.idEquipamentos)
            {
                List<EquipamentoAgenda> equipamentosAgenda = await _contexto.EquipamentoAgenda.Include((x) => x.agenda).Include((y) => y.equipamento)
                .Where(x => x.equipamentoid == idEquipamento && x.agenda.dataHoraChegada > DateTime.Now.AddHours(-3)
                 && ((dado.dataHoraChegada >= x.agenda.dataHoraChegada && dado.dataHoraChegada <= x.agenda.dataHoraSaida) ||
                 (dado.dataHoraSaida >= x.agenda.dataHoraChegada && dado.dataHoraSaida <= x.agenda.dataHoraSaida))).ToListAsync();

                if (equipamentosAgenda != null && equipamentosAgenda.Count > 0)
                {
                    return BadRequest(equipamentosAgenda[0].equipamento.nome + " ja tem um agendamento valido!");
                }
            }

            agenda.dataHoraChegada = dado.dataHoraChegada;
            agenda.dataHoraSaida = dado.dataHoraSaida;
            agenda.atividade = dado.atividade;
            agenda.empresaCurso = dado.empresaCurso;
            agenda.instituicaoCurso = dado.instituicaoCurso;
            agenda.laboratorioid = dado.idLaboratorio;
            agenda.responsavel = dado.responsavel;
            agenda.observacao = dado.observacao;
            agenda.aprovado = dado.aprovado;
            agenda.laboratorio = laboratorio;

            _contexto.Agenda.Add(agenda);

            _contexto.SaveChanges();

            foreach (int element in dado.idEquipamentos)
            {
                EquipamentoAgenda equipamentoAgenda = new EquipamentoAgenda();

                equipamentoAgenda.equipamentoid = element;
                equipamentoAgenda.agendaid = agenda.id;

                _contexto.EquipamentoAgenda.Add(equipamentoAgenda);
            };

            List<Usuario> usuario = await _contexto.Usuario.Where((x) => x.nome == agenda.responsavel).ToListAsync();

            if (usuario.Count > 0)
            {
                Email emailDestinatario = new Email();

                emailDestinatario.assunto = "Solicitação de Agendamento";
                emailDestinatario.corpo = "Você fez uma solicitação de agenda, um laboratorista ira avaliar, você sera avisado da agendamendo por aqui ou pode acompanhar pelo sistema.";
                emailDestinatario.destinatario = usuario[0].email;

                await EmailService.mandaEmail(emailDestinatario);
            }

            List<Usuario> laboratoristas = await _contexto.Usuario.Where((x) => x.nivel_acesso == "laboratorista").ToListAsync();

            foreach (Usuario labs in laboratoristas)
            {
                Email emailLaboratorista = new Email();

                emailLaboratorista.assunto = "Solicitação de Agendamento";
                emailLaboratorista.corpo = "Olá, " + dado.responsavel + " solicitou um agendamento e esta esperando você respondelo pelo sistema!";
                emailLaboratorista.destinatario = labs.email;

                await EmailService.mandaEmail(emailLaboratorista);
            }

            await _contexto.SaveChangesAsync();
            return Ok(agenda);
        }

        [HttpPost("aceitaAgenda/{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> aceitaUsuario(int id)
        {
            Agenda agenda = await _contexto.Agenda.FindAsync(id);
            if (agenda == null)
                return BadRequest("Nada Encontrado");

            agenda.aprovado = true;

            List<Usuario> usuario = await _contexto.Usuario.Where((x) => x.nome == agenda.responsavel).ToListAsync();

            if (usuario.Count > 0)
            {
                Email emailDestinatario = new Email();

                emailDestinatario.assunto = "Solicitação de Agendamento Aprovada";
                emailDestinatario.corpo = "Parabéns, " + usuario[0].nome + " sua solicitação de agendamento foi aceita. Inicio: " + agenda.dataHoraChegada + " Fim:" + agenda.dataHoraSaida;
                emailDestinatario.destinatario = usuario[0].email;

                await EmailService.mandaEmail(emailDestinatario);
            }

            await _contexto.SaveChangesAsync();
            return Ok("Aprovado Com Sucesso.");
        }

        [HttpPost("recusa/{id}/{motivo}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> recusaUsuario(int id, string motivo)
        {
            Agenda agenda = await _contexto.Agenda.FindAsync(id);
            if (agenda == null)
                return BadRequest("Nada Encontrado");

            agenda.deletado = true;

            List<Usuario> usuario = await _contexto.Usuario.Where((x) => x.nome == agenda.responsavel).ToListAsync();

            if (usuario.Count > 0)
            {
                Email emailDestinatario = new Email();

                emailDestinatario.assunto = "Solicitação de Agendamento Negada";
                emailDestinatario.corpo = usuario[0].nome + " infelizmente sua  solicitação de agendamento foi negada! Entre em contato com laboratoristas para verificar mais informações. Motivo: " + motivo;
                emailDestinatario.destinatario = usuario[0].email;

                await EmailService.mandaEmail(emailDestinatario);
            }

            await _contexto.SaveChangesAsync();

            return Ok("Negado Com Sucesso.");
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Agenda>> editarAgenda([FromBody] dtoEquipamentoAgenda dado, int id)
        {
            Laboratorio laboratorio = await _contexto.Laboratorio.FindAsync(dado.idLaboratorio);
            if (laboratorio == null)
                return BadRequest("Nada Encontrado");

            Agenda agenda = await _contexto.Agenda.FindAsync(id);

            dado.dataHoraChegada = dado.dataHoraChegada.AddHours(-3);

            dado.dataHoraSaida = dado.dataHoraSaida.AddHours(-3);

            if (dado.dataHoraChegada >= dado.dataHoraSaida)
            {
                return BadRequest("Data de Chegada não pode ser Maior ou Igual a de Saida!");
            }

            if (dado.dataHoraChegada < DateTime.Now.AddHours(-3))
            {
                return BadRequest("Data de chegada não pode ser anterior a agora!");
            }

            List<Agenda> agendas = await _contexto.Agenda.Where(x => x.laboratorioid == laboratorio.id && x.dataHoraChegada > DateTime.Now.AddHours(-3)
                && ((x.dataHoraChegada >= dado.dataHoraChegada && x.dataHoraChegada <= dado.dataHoraSaida) ||
                (x.dataHoraChegada <= dado.dataHoraSaida && x.dataHoraSaida >= dado.dataHoraChegada))).ToListAsync();

            if (agendas.Count > 0)
            {
                bool flagX = false;
                foreach (Agenda agendaX in agendas)
                {
                    if (!(agendaX.id == id) && (agendaX.laboratorioid == dado.idLaboratorio))
                    {
                        flagX = true;   
                    }
                }
                if (flagX) {
                    return BadRequest("Laboratorio já Agendado!");
                }
            }

            foreach (int idEquipamento in dado.idEquipamentos)
            {
                EquipamentoAgenda equipamentosAgenda = await _contexto.EquipamentoAgenda.Where(x => x.equipamentoid == idEquipamento && x.agendaid == id).FirstOrDefaultAsync();

                _contexto.EquipamentoAgenda.Remove(equipamentosAgenda);
                await _contexto.SaveChangesAsync();
            }

            foreach (int idEquipamento in dado.idEquipamentos)
            {
                List<EquipamentoAgenda> equipamentosAgenda = await _contexto.EquipamentoAgenda.Include((x) => x.agenda).Include((y) => y.equipamento)
                .Where(x => x.equipamentoid == idEquipamento && x.agenda.dataHoraChegada > DateTime.Now.AddHours(-3)
                 && ((dado.dataHoraChegada >= x.agenda.dataHoraChegada && dado.dataHoraChegada <= x.agenda.dataHoraSaida) ||
                 (dado.dataHoraSaida >= x.agenda.dataHoraChegada && dado.dataHoraSaida <= x.agenda.dataHoraSaida))).ToListAsync();

                if (equipamentosAgenda != null && equipamentosAgenda.Count > 0)
                {
                    return BadRequest(equipamentosAgenda[0].equipamento.nome + " ja tem um agendamento valido!");
                }
            }

            agenda.dataHoraChegada = dado.dataHoraChegada;
            agenda.dataHoraSaida = dado.dataHoraSaida;
            agenda.atividade = dado.atividade;
            agenda.empresaCurso = dado.empresaCurso;
            agenda.instituicaoCurso = dado.instituicaoCurso;
            agenda.laboratorioid = dado.idLaboratorio;
            agenda.responsavel = dado.responsavel;
            agenda.observacao = dado.observacao;
            agenda.aprovado = true;
            agenda.laboratorio = laboratorio;

            _contexto.SaveChanges();

            foreach (int element in dado.idEquipamentos)
            {
                EquipamentoAgenda equipamentoAgenda = new EquipamentoAgenda();

                equipamentoAgenda.equipamentoid = element;
                equipamentoAgenda.agendaid = agenda.id;

                _contexto.EquipamentoAgenda.Add(equipamentoAgenda);
            };

            await _contexto.SaveChangesAsync();
            return Ok(agenda);
        }

        [HttpDelete("logicamente/{id}")]
        [Authorize]
        public async Task<ActionResult<Agenda>> DeletalaboratorioLogicamente(int id)
        {
            var laboratorio = await _contexto.Usuario.FindAsync(id);
            if (laboratorio == null)
                return BadRequest("Nada Encontrado");

            laboratorio.deletado = true;

            await _contexto.SaveChangesAsync();

            return Ok(laboratorio);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Agenda>>> deletarlaboratorio(int id)
        {
            var dado = await _contexto.Agenda.FindAsync(id);
            if (dado == null)
                return BadRequest("Nada encontrado!");

            _contexto.Agenda.Remove(dado);
            await _contexto.SaveChangesAsync();

            return Ok(await _contexto.Agenda.ToListAsync());
        }
    }
}