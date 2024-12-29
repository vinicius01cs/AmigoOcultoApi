using System.ComponentModel.DataAnnotations;

namespace AmigoOculto.ViewModels
{
    public class CreateGrupoViewModel
    {
        [Required]
        public string NomeGrupo { get; set; }
    }
}
