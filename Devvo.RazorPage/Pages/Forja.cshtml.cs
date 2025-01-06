using Devvo.Application.DTOs.Anel;
using Devvo.Application.Interfaces;
using Devvo.Application.ViewModel;
using Devvo.CrossCutting.Util.Enum;
using Devvo.RazorPage.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace Devvo.RazorPage.Pages
{
    [Authorize]
    public class ForjaModel : PageModel
    {
        private readonly IAnelAppService _anelService;
        private InfoUsuarioLogadoModel usuarioLogado;

        public ForjaModel(IAnelAppService anelAppService)
        {
            _anelService = anelAppService;
        }

        public List<AnelViewModel> Aneis { get; set; } = new List<AnelViewModel>();

        [BindProperty]
        public bool EsconderMensagemEm3Segundos { get; set; } = false;

        [BindProperty]
        public bool mostrarMensagem { get; set; } = false;

        [BindProperty]
        public string TituloMensagemErro { get; set; } =  string.Empty;

        public string MensagemErro { get; set; } = string.Empty;

        [BindProperty]
        public Guid AnelId { get; set; }

        [BindProperty]
        public AnelModel anelModel { get; set; } = new AnelModel();

        [BindProperty]
        public EditarAnelDTO editarAnelDTO { get; set; }

        public async Task OnGet()
        {
            var buscaInfo = BuscarInfoUsuarioLogado();

            if (buscaInfo.Id is null)
                RedirectToPage("/login");

            await OnGetAneis();
        }

        public async Task<IActionResult> OnPostConfirmDelete()
        {
            var buscaInfo = BuscarInfoUsuarioLogado();
            if (buscaInfo is null)
                RedirectToPage("/login");
            var usuario = buscaInfo;

            var resultado = await _anelService.DeletarElementoAsync(usuario.Id, AnelId);

            if (!resultado.Sucesso) { }

            return RedirectToPage("/Forja");
        }

        public async Task OnGetAneis()
        {
            var buscaInfo = BuscarInfoUsuarioLogado();
            if (buscaInfo is null)
                RedirectToPage("/login");
            var usuario = buscaInfo;

            var resultado = await _anelService.BuscarTodosOsElementosAsync(usuario.Id);

            if (!resultado.Sucesso) { }

            Aneis = (List<AnelViewModel>)resultado.Dados;
        }

        public async Task<IActionResult> OnPostForjarAnel()
        {

            var buscaInfo = BuscarInfoUsuarioLogado();
            if (buscaInfo is null)
                RedirectToPage("/login");
            string usuarioId = buscaInfo.Id ?? "";

            var anelDto = new CadastrarAnelDTO(anelModel.Nome, anelModel.Poder,anelModel.Portador,anelModel.ForjadoPor, anelModel.Tipo, usuarioId);

            var resultado = await _anelService.CadastrarElementoAsync(anelDto);

            if (!resultado.Sucesso)
            {
                mostrarMensagem = true;
                MensagemErro = resultado.MensagemUsuario;
                EsconderMensagemEm3Segundos = true;
                TituloMensagemErro = resultado.Dados;
                await OnGetAneis();
                return Page();
            }
            await OnGetAneis();
            return Page();
        }

        public async Task<IActionResult> OnPostEditarAnel()
        {
            var buscaInfo = BuscarInfoUsuarioLogado();
            if (buscaInfo is null)
                RedirectToPage("/login");
            string usuarioId = buscaInfo.Id ?? "";

            var editarAnelDto = new EditarAnelDTO(anelModel.Nome, anelModel.Portador, anelModel.Poder,  anelModel.ForjadoPor, anelModel.Tipo);

            var resultado = await _anelService.EditarElementoAsync(usuarioId, anelModel.Id, editarAnelDto);

            if (!resultado.Sucesso)
            {
                mostrarMensagem = true;
                MensagemErro = resultado.MensagemUsuario;
                EsconderMensagemEm3Segundos = true;
                TituloMensagemErro = resultado.Dados;
                await OnGetAneis();
                return Page();
            }
            await OnGetAneis();
            return Page();
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

