using Devvo.CrossCutting.Util.Enum;
using System.ComponentModel.DataAnnotations;

namespace Devvo.Application.DTOs.Anel
{
    public class EditarAnelDTO
    {
        public EditarAnelDTO() { }  
        public EditarAnelDTO(string nome, string portador, string poder, string forjadorPor, EAnel tipo) {

            Nome = nome;
            Portador = portador;
            Poder = poder;
            ForjadoPor = forjadorPor;
            Tipo = tipo;      
        }
        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Poder { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string Portador { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public string ForjadoPor { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        public EAnel Tipo { get; set; }
    }
}
