using System.ComponentModel.DataAnnotations;

namespace Devvo.RazorPage.Models
{
    public class CadastrarUsuarioModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um Email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "A Senha deve ter entre 4 e 10 caracteres.")]
        public string Senha { get; set; }

        public CadastrarUsuarioModel(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
