namespace Accounting.Common
{
    public class Pagination<T>
    {
        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int? ItemPerPage { get; set; }

        public int? PageIndex { get; set; }

        public int? NextPage { get; set; }

        public int? PrevPage { get; set; }

        public List<T> Items { get; set; }

        public Pagination(int totalItems, int? pageIndex, int? itemPerPage, List<T> items)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemPerPage);

            var nextPage = pageIndex + 1;

            var prevPage = pageIndex - 1;


            if (pageIndex < 1 || totalPages == 0)
            {
                pageIndex = 1;
                prevPage = pageIndex;
            }
            else if (pageIndex == totalPages)
            {
                nextPage = pageIndex;
            }
            else if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }
            else if (pageIndex == 1)
            {
                prevPage = pageIndex;
            }

            items = items.Skip((int)((pageIndex - 1) * itemPerPage)).Take((int)itemPerPage).ToList();

            TotalItems = totalItems;
            TotalPages = totalPages;
            PageIndex = pageIndex;
            ItemPerPage = itemPerPage;
            NextPage = nextPage;
            PrevPage = prevPage;
            Items = items;
        }
    }
}
