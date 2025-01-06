using Devvo.CrossCutting.Util.Enum;
using Devvo.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Devvo.Application.DTOs.Anel
{
    public class CadastrarAnelDTO
    {
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

        public string UsuarioId { get; set; } = string.Empty;


        public CadastrarAnelDTO(string nome, string poder, string portador, string forjadoPor, EAnel tipo, string usuarioId)
        {
            Nome = nome;
            Poder = poder;
            Portador = portador;
            ForjadoPor = forjadoPor;
            Tipo = tipo;
            UsuarioId = usuarioId;
        }
    }
}
