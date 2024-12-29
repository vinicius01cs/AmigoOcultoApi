using Microsoft.AspNetCore.Mvc;
using AmigoOculto.Models;
using AmigoOculto.Data;
using Microsoft.EntityFrameworkCore;
using AmigoOculto.ViewModels;

namespace AmigoOculto.Controllers
{
    [ApiController]
    [Route("v1")]
    public class GrupoController : ControllerBase
    {
        [HttpGet]
        [Route("grupos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var grupos = await context.Grupos.AsNoTracking().ToListAsync();
            return Ok(grupos);
        }

        [HttpGet]
        [Route("grupos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var grupo = await context.Grupos.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            return grupo == null ? NotFound() : Ok(grupo);
        }

        [HttpPost("grupos")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateGrupoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var grupo = new Grupo
            {
                NomeGrupo = model.NomeGrupo
            };
            try
            {
                await context.Grupos.AddAsync(grupo); //Salva apenas na memoria
                await context.SaveChangesAsync(); //Salva no banco
                return Created($"v1/grupos/{grupo.id}", grupo);
            }catch(Exception e)
            {
                return BadRequest();
            }

        }

    }
}
