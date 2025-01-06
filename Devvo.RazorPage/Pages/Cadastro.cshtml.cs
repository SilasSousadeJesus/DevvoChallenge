using Devvo.RazorPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Devvo.Application.Interfaces;
using Devvo.Application.DTOs.Usuario;

namespace Devvo.RazorPage.Pages
{
    public class Cadastro : PageModel
    {

        private readonly IUsuarioAppService _usuarioAppService;

        public Cadastro(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }


        [BindProperty]
        public bool mostrarMensagem { get; set; } = false;

        public string MensagemErro { get; set; } = string.Empty;

        [BindProperty]
        public FormularioCadastroModel FormularioCadastroModel { get; set; } = new FormularioCadastroModel();

        [BindProperty]
        public bool EsconderMensagemEm3Segundos { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            var cadastro = new CadastrarUsuarioDTO(
                FormularioCadastroModel.Nome,
                FormularioCadastroModel.Email,
                FormularioCadastroModel.Senha
             );

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var resultado = await _usuarioAppService.Cadastrar(cadastro);


            if (!resultado.Sucesso)
            {
                mostrarMensagem = true;
                MensagemErro = resultado.MensagemUsuario;
                EsconderMensagemEm3Segundos = true;
                return Page();
            }

            return RedirectToPage("/login");
        }

    }
}
