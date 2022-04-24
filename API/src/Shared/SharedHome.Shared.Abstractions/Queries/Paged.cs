namespace SharedHome.Shared.Abstractions.Queries
{
    public class Paged<T> : PagedBase
    {
        public List<T> Items { get; set; } = new();

        public Paged()
        {
            CurrentPage = 1;
            TotalPages = 1;
            PageSize = 25;
        }

        public Paged(List<T> items, int currentPage, int pageSize,
            int totalPages, int totalItems) :
            base(currentPage, pageSize, totalPages, totalItems)
        {
            Items = items;
        }
    }
}
