using Devvo.RazorPage.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Devvo.RazorPage.Pages
{
    public class SobreAneisModel : PageModel
    {
        private InfoUsuarioLogadoModel usuarioLogado;
        public void OnGet()
        {

        }    

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete(".AspNetCore.Antiforgery.IVcQjimEJAo");
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            Response.Cookies.Delete("AuthToken");

            return RedirectToPage("/Login");
        }

    }
}
