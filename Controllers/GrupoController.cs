﻿using Microsoft.AspNetCore.Mvc;
using AmigoOculto.Models;
using AmigoOculto.Data;
using AmigoOculto.DTO;
using Microsoft.EntityFrameworkCore;
using AmigoOculto.ViewModels;
using AmigoOculto.Services;

namespace AmigoOculto.Controllers
{
    [ApiController]
    [Route("v1")]
    public class GrupoController : ControllerBase
    {

        private readonly GrupoServices grupoServices;

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

            if (grupo == null)
            {
                return NotFound();
            }
            var participantes = await context.Participantes.AsNoTracking().Where(x => x.GrupoId == id).ToListAsync();

            var grupoDetails = new GrupoDetailsDto
            {
                Id = grupo.id,
                Nome = grupo.NomeGrupo,
                Participantes = participantes
            };

            return Ok(grupoDetails);
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
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        [HttpPost("grupos/{grupoId}/participantes")]
        public async Task<IActionResult> AddParticipanteAsync([FromServices] AppDbContext context, [FromRoute] int grupoId, [FromBody] AddParticipanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var participante = new Participante
            {
                Nome = model.Nome,
                Email = model.Email,
                GrupoId = grupoId

            };
            try
            {
                await context.Participantes.AddAsync(participante);
                await context.SaveChangesAsync();
                return Created($"v1/grupos/{grupoId}/{participante.Id}", participante);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("grupos/{grupoId}/sorteio")]
        public async Task<IActionResult> SortearGrupoAsync([FromServices] AppDbContext context, [FromRoute] int grupoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var participantes = await grupoServices.ObterParticipantesSorteioAsync(context, grupoId);

            if (participantes.Count < 3)
            {
                return BadRequest(new { message = "O grupo precisa ter ao menos 3 participantes para realizar o sorteio" });
            }

            var sorteios = grupoServices.SortearGrupo(participantes, grupoId);

            try
            {
                await context.Sorteios.AddRangeAsync(sorteios);
                await context.SaveChangesAsync();
                return Ok(new { message = "Sorteio realizado com sucesso ! " });

            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpGet("grupos/{grupoId}/sorteio")]
        public async Task<IActionResult> GetSorteioAsync([FromServices] AppDbContext context, [FromRoute] int grupoId)
        {
            var sorteio = await context.Sorteios
                .Where(x => x.GrupoId == grupoId)
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
