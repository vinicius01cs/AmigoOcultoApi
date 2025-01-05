using AmigoOculto.Data;
using AmigoOculto.DTO;
using AmigoOculto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmigoOculto.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ParticipanteController : ControllerBase
    {
        [HttpGet]
        [Route("participantes")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, [FromQuery] string email) {
            var sorteio = await context.Sorteios
               .Where(x => x.Participante.Email == email)
               .Select(x => new GrupoSorteioDto
               {
                   Id = x.Id,
                   GrupoId = x.GrupoId,
                   ParticipanteId = x.Participante.Id,
                   ParticipanteNome = x.Participante.Nome,
                   ParticipanteEmail = x.Participante.Email,
                   AmigoOcultoId = x.AmigoOcultoId,
                   AmigoOcultoNome = x.AmigoOculto.Nome,
                   AmigoOcultoEmail = x.AmigoOculto.Email
               }).ToListAsync();

            return Ok(sorteio);
        }
    }
}
