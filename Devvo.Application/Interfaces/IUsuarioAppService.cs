using Devvo.Application.DTOs.Usuario;
using Devvo.CrossCutting.Extensions;

namespace Devvo.Application.Interfaces
{
    public interface IUsuarioAppService 
    {
        Task<RetornoGenerico> Cadastrar(CadastrarUsuarioDTO loginDTO);
        Task<RetornoGenerico> BuscarUmUsuario(string UsuarioId);
        Task<RetornoGenerico> BuscarTodosOsUsuario();
        Task<RetornoGenerico> EditarUsuario(string usuarioId, EditarUsuarioDTO editarUsuarioDTO);
        Task<RetornoGenerico> DeletarUsuario(string UsuarioId);
    }
}
