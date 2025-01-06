namespace Devvo.CrossCutting.Extensions
{
    public class PaginacaoDeResultados
    {
        public List<dynamic> Items { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PaginacaoDeResultados(List<dynamic> items, int totalItems, int page, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }

        public class PaginacaoHelper
        {
            public static PaginacaoDeResultados Paginate(List<dynamic> source, int page, int pageSize)
            {
                int totalItems = source.Count;
                List<dynamic> items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return new PaginacaoDeResultados(items, totalItems, page, pageSize);
            }
        }
    }
}
