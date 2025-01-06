using System.ComponentModel.DataAnnotations;

namespace Devvo.Application.DTOs.Autenticacao
{
    public class LoginDTO
    {
        public LoginDTO() { }
        public LoginDTO(string email, string senha) {
            Email = email;
            Senha = senha;     
        }


        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        [EmailAddress(ErrorMessage = "O Campo {0} é inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "0 Campo {0} é Obrigatorio")]
        [StringLength(20, ErrorMessage = "O Campo {0} deve ter até 20 caracteres", MinimumLength = 4)]
        public string Senha { get; set; } = string.Empty;
    }
}
