using Devvo.CrossCutting.Extensions;

namespace Devvo.Application.Interfaces.InterfaceBase
{
    public interface IAppServiceCriacao<TCriacaoDTO> where TCriacaoDTO : class
    {
        Task<RetornoGenerico> CadastrarElementoAsync(TCriacaoDTO elementoDTO);
    }
}
