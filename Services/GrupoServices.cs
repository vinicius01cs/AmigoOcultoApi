using AmigoOculto.Data;
using AmigoOculto.Models;
using Microsoft.EntityFrameworkCore;

namespace AmigoOculto.Services
{
    public class GrupoServices
    {
        public async Task<List<Participante>> ObterParticipantesSorteioAsync(AppDbContext context, int grupoId)
        {
            return await context.Participantes.AsNoTracking().Where(p => p.GrupoId == grupoId).ToListAsync(); 
        }

        public List<Sorteio> SortearGrupo(List<Participante> participantes, int grupoId)
        {
            var random = new Random();
            var embaralhados = participantes.OrderBy(x => random.Next()).ToList();

            var sorteios = new List<Sorteio>();
            for (int i = 0; i < embaralhados.Count; i++)
            {
                var participante = embaralhados[i];
                var amigoOculto = embaralhados[(i + 1) % embaralhados.Count];

                sorteios.Add(new Sorteio
                {
                    GrupoId = grupoId,
                    ParticipanteId = participante.Id,
                    AmigoOcultoId = amigoOculto.Id
                });
            }
            return sorteios;
        }
    }
}
