namespace SIMPLE_WEB_API.Paginationfilter
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecord { get; private set; }


        public bool HasPrevPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int PageSize)
        {
            TotalRecord = count;
            PageIndex = pageIndex;
            PageSize = (int)Math.Ceiling(count / (double) PageSize);

            this.AddRange(items);
        }

        public static PaginatedList<T> Create(List<T> items, int count, FilterOPtions filter)
        {
            return items is null ? new PaginatedList<T>(Enumerable.Empty<T>().ToList(), 0, 0, 0) :
                new(items, count, filter.PageSize, filter.PageIndex);
        }
    }
}