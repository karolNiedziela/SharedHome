namespace SharedHome.Shared.Abstractions.Queries
{
    public interface IPagedQuery<TResponse> : IQuery<TResponse>
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
