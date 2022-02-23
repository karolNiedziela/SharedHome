namespace SharedHome.Shared.Abstractions.Responses
{
    public class Response<T>
    {
        public T Data { get; set; } = default!;

        public string? Message { get; set; } = string.Empty;

        public Response()
        {

        }

        public Response(T data, string? message = null)
        {
            Data = data;
            Message = message;
        }
    }
}
