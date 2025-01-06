using System.ComponentModel.DataAnnotations;

namespace Devvo.RazorPage.Models
{
    public class FormularioCadastroModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(3, ErrorMessage = "A senha deve ter pelo menos 3 caracteres.")]
        public string Senha { get; set; }
    }
}
