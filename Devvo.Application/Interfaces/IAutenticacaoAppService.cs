using Devvo.Application.DTOs.Autenticacao;
using Devvo.CrossCutting.Extensions;

namespace Devvo.Application.Interfaces
{
    public interface IAutenticacaoAppService
    {
        Task<RetornoGenerico> Login(LoginDTO loginDTO);
    }
}
