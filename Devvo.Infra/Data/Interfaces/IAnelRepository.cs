using Devvo.CrossCutting.Extensions;
using Devvo.CrossCutting.Util.Enum;
using Devvo.Domain.Entities;

namespace Devvo.Infra.Data.Interfaces
{
    public interface IAnelRepository : IRepository<Anel>
    {
        Task<PaginacaoDeResultados> BuscarPorFiltros(
            string nome, 
            string portador, 
            string forjador, 
            EAnel tipo, 
            int pagina, 
            int itensPorPagina, 
            string usuarioId
            );
        Task<int> BuscarTodosPorTipoAsync(string id, EAnel tipo);
    }
}
