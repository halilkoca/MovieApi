namespace App.Entity.Filters
{
    public class BaseFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int? OrderBy { get; set; }
        public string OrderColumn { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int PageIndex
        {
            get
            {
                if (PageNumber > 0)
                    return PageNumber - 1;

                return 0;
            }
        }
    }
}
