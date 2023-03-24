
namespace UniversityManagementSystemPortal.Helpers.Paging
{

    public class PaginatedList<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Search { get; private set; }
        public string ColumnName { get; set; }
        public string SortBy { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }

        public bool HasPreviousPage { get { return CurrentPage > 1; } }
        public bool HasNextPage { get { return CurrentPage < TotalPages; } }


        public PaginatedList<T> SetValues(List<T> items, int currentPage, int pageSize, string search, string columnName, string sortBy, int totalPages, int count)
        {
            
            CurrentPage = currentPage;
            PageSize = pageSize;
            Search = search;
            ColumnName = columnName;
            SortBy = sortBy;
            TotalPages = totalPages;
            Count = count;
            Items = items;
            return this;

        }

    }
    public static class PaginationHelper
    {
        public static PaginatedList<T> Create<T>(IQueryable<T> source, PaginatedViewModel paginatedViewModel)
        {
            var count = source.Count();
            var totalPages = (int)Math.Ceiling(count / (double)paginatedViewModel.PageSize);
            var items = source.Skip((paginatedViewModel.PageNumber - 1) * paginatedViewModel.PageSize)
                .Take(paginatedViewModel.PageSize).ToList();
            var paginatedResult = new PaginatedList<T>();
            paginatedResult.SetValues(items,
                 paginatedViewModel.PageNumber,
                 paginatedViewModel.PageSize,
                 paginatedViewModel._columnName,
                 paginatedViewModel.search,
                 paginatedViewModel.SortBy,
                 totalPages,
               count
               );

            return paginatedResult;
        }
    }

}

