using Devvo.Application.DTOs.Autenticacao;
using Devvo.Application.DTOs.Usuario;
using Devvo.Application.Interfaces;
using Devvo.RazorPage.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Devvo.RazorPage.Pages
{
    public class Login : PageModel
    {

        private readonly IAutenticacaoAppService _autenticacaoAppService;

        public Login(IAutenticacaoAppService autenticacaoAppService)
        {
            _autenticacaoAppService = autenticacaoAppService;
        }


        [BindProperty]
        public bool mostrarMensagem { get; set; } = false;

        public string MensagemErro { get; set; } = string.Empty;

        [BindProperty]
        public FormularioLoginModel FormularioLoginModel { get; set; } = new FormularioLoginModel();

        [BindProperty]
        public bool EsconderMensagemEm3Segundos { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var login = new LoginDTO(
                    FormularioLoginModel.Email,
                    FormularioLoginModel.Senha
                );

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var resultado = await _autenticacaoAppService.Login(login);

            if (!resultado.Sucesso)
            {
                mostrarMensagem = true;
                MensagemErro = resultado.MensagemUsuario;
                EsconderMensagemEm3Segundos = true;
                return Page();
            }

            var token = resultado.Dados.Token;
            var handler = new JwtSecurityTokenHandler();

            try
            {
                if (token.Split('.').Length != 3)
                {
                    throw new Exception("Token JWT está malformado");
                }

                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    throw new Exception("Falha ao ler o token JWT");
                }

                var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                var email = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                var nome = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;

                var roles = jsonToken?.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userId), 
                    new Claim(JwtRegisteredClaimNames.Email, email), 
                    new Claim(JwtRegisteredClaimNames.Name, nome), 
                    new Claim("role", string.Join(",", roles)) 
                };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, 
                    Expires = DateTimeOffset.Now.AddDays(1) 
                };

                Response.Cookies.Append("AuthToken", token, options);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/forja");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar o token JWT: {ex.Message}");
            }
        }

    }
}
