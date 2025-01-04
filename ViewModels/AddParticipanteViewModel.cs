using System.ComponentModel.DataAnnotations;

namespace AmigoOculto.ViewModels
{
    public class AddParticipanteViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
