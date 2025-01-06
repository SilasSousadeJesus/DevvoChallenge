using System.ComponentModel.DataAnnotations;

namespace Devvo.Application.DTOs.Usuario
{
    public class EditarUsuarioDTO
    {
        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        [StringLength(20, ErrorMessage = "O Campo {0} deve ter até 20 caracteres", MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        [EmailAddress(ErrorMessage = "O Campo {0} é inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        [StringLength(20, ErrorMessage = "O Campo {0} deve ter até 20 caracteres", MinimumLength = 4)]
        public string Senha { get; set; } = string.Empty;
    }
}
