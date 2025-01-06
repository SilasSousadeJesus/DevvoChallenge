using System.ComponentModel.DataAnnotations;

namespace Devvo.RazorPage.Models
{
    public class FormularioLoginModel
    {
        public FormularioLoginModel(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public FormularioLoginModel()
        {
        }
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(3, ErrorMessage = "A senha deve ter pelo menos 3 caracteres.")]
        public string Senha { get; set; }
    }
}
