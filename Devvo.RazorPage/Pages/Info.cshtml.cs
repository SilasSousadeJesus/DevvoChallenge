using Devvo.RazorPage.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Devvo.Application.Interfaces;

namespace Devvo.RazorPage.Pages
{
    public class InfoModel : PageModel
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public InfoModel(IUsuarioAppService usuarioAppService) {
            _usuarioAppService = usuarioAppService;    
        }

        [BindProperty]
        public InfoUsuarioLogadoModel usuarioLogado { get; set; } = new InfoUsuarioLogadoModel("","","");
        public void OnGet()
        {
            var buscaInfo = BuscarInfoUsuarioLogado();

            if (buscaInfo.Id is null)
                RedirectToPage("/login");

            usuarioLogado.Email = buscaInfo.Email;
            usuarioLogado.Nome = buscaInfo.Nome;
            usuarioLogado.Id = buscaInfo.Id;
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete(".AspNetCore.Antiforgery.IVcQjimEJAo");
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            Response.Cookies.Delete("AuthToken");

            return RedirectToPage("/Login");
        }

        public InfoUsuarioLogadoModel? BuscarInfoUsuarioLogado()
        {
            var userClaims = HttpContext.User;
            if (userClaims.Identity.IsAuthenticated)
            {
                return new InfoUsuarioLogadoModel(
                   userClaims.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value,
                   userClaims.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value,
                   userClaims.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value);
            }
            else
            {
                return null;
            }
        }
        public async Task<IActionResult> OnPostDeletarConta()
        {
            var buscaInfo = BuscarInfoUsuarioLogado();

            if (buscaInfo.Id is null)
                RedirectToPage("/login");

            var resposta= await _usuarioAppService.DeletarUsuario(buscaInfo.Id);

            if (!resposta.Sucesso) { }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete(".AspNetCore.Antiforgery.IVcQjimEJAo");
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            Response.Cookies.Delete("AuthToken");

            return RedirectToPage("/Login");
        }

    }
}
