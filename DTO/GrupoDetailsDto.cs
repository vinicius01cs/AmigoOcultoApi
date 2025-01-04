using AmigoOculto.Models;

namespace AmigoOculto.DTO
{
    public class GrupoDetailsDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Participante> Participantes { get; set; }

    }
}
