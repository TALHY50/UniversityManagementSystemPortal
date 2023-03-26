using System.Drawing.Printing;

namespace UniversityManagementSystemPortal.Helpers.Paging
{
    public class PaginatedViewModel
    {
        //const int maxPageSize = 50;
        private string _search = string.Empty;
        private string _sortBy = string.Empty;
        public string _columnName = string.Empty;
        private int _pageNumber { get; set; }
        private int _pageSize = 10;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        public string columnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
            }
        }
        public string search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
            }
        }

        public string SortBy
        {
            get { return _sortBy; }
            set { _sortBy = value; }
        }
    }

}
