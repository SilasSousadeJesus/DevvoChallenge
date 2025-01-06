using Devvo.CrossCutting.Extensions;

namespace Devvo.Application.Interfaces.InterfaceBase
{
    public interface IAppService<TCriacaoDTO, TAtualizacaoDTO> : IAppServiceCriacao<TCriacaoDTO>, IAppServiceAtualizacao<TAtualizacaoDTO>
        where TCriacaoDTO : class
        where TAtualizacaoDTO : class
    {
        Task<RetornoGenerico> BuscarTodosOsElementosAsync(string id);
        Task<RetornoGenerico> BuscarUmElementoAsync(string usuarioId, Guid elementoId);
        Task<RetornoGenerico> DeletarElementoAsync(string idPatrono, Guid idElemento);
    }
}
