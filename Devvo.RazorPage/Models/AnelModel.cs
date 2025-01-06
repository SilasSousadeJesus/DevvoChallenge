using Devvo.CrossCutting.Util.Enum;
using System.ComponentModel.DataAnnotations;

namespace Devvo.RazorPage.Models
{
    public class AnelModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Poder { get; set; } = string.Empty;
        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Portador { get; set; } = string.Empty;
        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string ForjadoPor { get; set; } = string.Empty;
        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Imagem { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public EAnel Tipo { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
    }
}
