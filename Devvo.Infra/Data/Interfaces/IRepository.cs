namespace Devvo.Infra.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> BuscarTodosOsElementosAsync(string id);
        Task<T> BuscarUmElementoAsync(string idPatrono, Guid id);
        Task<bool> CadastrarElementoAsync(T elemento);
        Task<bool> DeletarElementoAsync(T elemento);
        Task<bool> EditarElementoAsync(T elemento);
    }
}
