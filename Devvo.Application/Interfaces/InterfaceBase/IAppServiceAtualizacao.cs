using Devvo.CrossCutting.Extensions;

namespace Devvo.Application.Interfaces.InterfaceBase
{
    public interface IAppServiceAtualizacao<TAtualizacaoDTO> where TAtualizacaoDTO : class
    {
        Task<RetornoGenerico> EditarElementoAsync(string idPatrono, Guid elementoId, TAtualizacaoDTO elementoDTO);
    }
}
