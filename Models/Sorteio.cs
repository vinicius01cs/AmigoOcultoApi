namespace AmigoOculto.Models
{
    public class Sorteio
    {
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public int ParticipanteId { get; set; }
        public int AmigoOcultoId { get; set; }
        public Grupo Grupo { get; set; }
        public Participante Participante { get; set; }
        public Participante AmigoOculto { get; set; }
    }
}
