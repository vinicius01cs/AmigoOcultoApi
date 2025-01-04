using Microsoft.EntityFrameworkCore;
using AmigoOculto.Models;

namespace AmigoOculto.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Sorteio> Sorteios { get; set; }

    }
}
