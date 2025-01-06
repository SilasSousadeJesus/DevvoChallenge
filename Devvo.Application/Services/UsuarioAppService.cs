using Devvo.Application.DTOs.Usuario;
using Devvo.Application.Interfaces;
using Devvo.CrossCutting.Extensions;
using Devvo.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Devvo.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {

        private readonly UserManager<Usuario> _userManager;

        public UsuarioAppService(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RetornoGenerico> BuscarUmUsuario(string usuarioId)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);

            var resultado = user == null ? false : true;

            return new RetornoGenerico
            {
                Sucesso = resultado,
                HttpStatusCode = resultado ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound,
                MensagemSistema = resultado ? "Usuario não encontrado" : "Usuario Encontrado",
                MensagemUsuario = resultado ? "Usuario não encontrado" : "Usuario Encontrado",
                Dados = user
            };
        }

        public async Task<RetornoGenerico> Cadastrar(CadastrarUsuarioDTO cadastroUsuarioDTO)
        {
            var identityUser = new Usuario
            {
                Nome = cadastroUsuarioDTO.Nome,
                UserName = cadastroUsuarioDTO.Email,
                Email = cadastroUsuarioDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, cadastroUsuarioDTO.Senha);
            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }

            return new RetornoGenerico(result.Succeeded, 
                result.Succeeded ? "Usuario criado com sucesso" : "Usuario não pode ser criado", 
                result.Succeeded ? "Usuario criado com sucesso" : "Usuario não pode ser criado",
                result.Succeeded ? System.Net.HttpStatusCode.Created : System.Net.HttpStatusCode.BadRequest);
        }

        public async Task<RetornoGenerico> DeletarUsuario(string usuarioId)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            var mensagem = user == null ? "Usuario não encontrado" : "Usuario deletado";
            var sucesso = user != null;

            if (sucesso)
            {
                var resultado = await _userManager.DeleteAsync(user);
            }

            return new RetornoGenerico
            {
                Sucesso = sucesso,
                HttpStatusCode = sucesso ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound,
                MensagemSistema = mensagem,
                MensagemUsuario = mensagem,
                Dados = null
            };
        }

        public async Task<RetornoGenerico> EditarUsuario(string usuarioId, EditarUsuarioDTO editarUsuarioDTO)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user == null)
            {
                return new RetornoGenerico
                {
                    Sucesso = false,
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                    MensagemSistema = "Usuário não encontrado",
                    MensagemUsuario = "Usuário não encontrado",
                    Dados = null
                };
            }

            user.Nome = editarUsuarioDTO.Nome != user.Nome ? editarUsuarioDTO.Nome : user.Nome;
            user.Email = editarUsuarioDTO.Email != user.Email ? editarUsuarioDTO.Email : user.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new RetornoGenerico
                {
                    Sucesso = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    MensagemSistema = "Erro ao atualizar usuário",
                    MensagemUsuario = "Erro ao atualizar usuário",
                    Dados = null
                };
            }

            return new RetornoGenerico
            {
                Sucesso = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                MensagemSistema = "Usuário atualizado com sucesso",
                MensagemUsuario = "Usuário atualizado com sucesso",
                Dados = user
            };
        }

        public async Task<RetornoGenerico> BuscarTodosOsUsuario()
        {
            throw new NotImplementedException();
        }
    }
}
