using System.Text.Json.Serialization;

namespace SharedHome.Shared.Application.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; } = string.Empty;

        public ApiResponse()
        {

        }

        public ApiResponse(T data, string? message = null)
        {
            Data = data;
            Message = message;
        }
    }
}
