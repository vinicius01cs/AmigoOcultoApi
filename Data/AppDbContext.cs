using Microsoft.EntityFrameworkCore;
using AmigoOculto.Model;

namespace AmigoOculto.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Grupo> Grupos { get; set; }

    }
}
