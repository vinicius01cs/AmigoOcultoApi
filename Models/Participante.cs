namespace AmigoOculto.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
    }
}
