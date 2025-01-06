using Devvo.CrossCutting.Extensions;
using Devvo.CrossCutting.Util.Enum;
using Devvo.Domain.Entities;
using Devvo.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Devvo.Infra.Data.Repositories
{
    public class AnelRepository : IAnelRepository
    {
        private readonly ApplicationDbContext _context;

        public AnelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoDeResultados> BuscarPorFiltros(string nome, 
                string portador,
                string forjador, 
                EAnel tipo, 
                int pagina, 
                int itensPorPagina, 
                string usuarioId
            )
        {

           var listaAneis = await _context.Anel
            .Where(x => x.UsuarioId == usuarioId &&
                (string.IsNullOrEmpty(nome) || x.Nome.ToLower().Contains(nome.ToLower())) &&
                (string.IsNullOrEmpty(portador) || x.Portador == portador) &&
                (string.IsNullOrEmpty(forjador) || x.ForjadoPor == forjador)).ToListAsync();


            if (tipo != EAnel.NaoIdentificado) {
                listaAneis = listaAneis.Where(x => x.Tipo == tipo).ToList();
            }

            List<dynamic> lista = listaAneis.Cast<dynamic>().ToList();

            PaginacaoDeResultados paginado = PaginacaoDeResultados.PaginacaoHelper.Paginate(lista, pagina, itensPorPagina);

            return paginado;
        }


        public async Task<int> BuscarTodosPorTipoAsync(string id, EAnel tipo)
        {
            return await _context.Set<Anel>()
                 .Where(a => a.UsuarioId == id && a.Tipo == tipo)
                 .CountAsync();
        }

        public async Task<List<Anel>> BuscarTodosOsElementosAsync(string id)
        {
            return await _context.Set<Anel>()
                 .Where(b => b.UsuarioId == id)
                 .ToListAsync();
        }

        public async Task<Anel> BuscarUmElementoAsync(string idPatrono, Guid id)
        {
            return await _context.Set<Anel>().Where(x => x.UsuarioId == idPatrono && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CadastrarElementoAsync(Anel elemento)
        {
            try
            {
                await _context.Set<Anel>().AddAsync(elemento);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false; 
            }
        }

        public async Task<bool> DeletarElementoAsync(Anel elemento)
        {
            try
            {
                _context.Set<Anel>().Remove(elemento);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditarElementoAsync(Anel elemento)
        {
            try
            {
                var existingEntity = _context.Set<Anel>().Local.FirstOrDefault(b => b.Id == elemento.Id);
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }

                _context.Set<Anel>().Update(elemento);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
