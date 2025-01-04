using AmigoOculto.Models;

namespace AmigoOculto.DTO
{
    public class GrupoSorteioDto
    {
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public int ParticipanteId { get; set; }
        public string ParticipanteNome { get; set; }
        public string ParticipanteEmail { get; set; }
        public int AmigoOcultoId { get; set; }
        public string AmigoOcultoNome { get; set; }
        public string AmigoOcultoEmail { get; set; }
    }
}
