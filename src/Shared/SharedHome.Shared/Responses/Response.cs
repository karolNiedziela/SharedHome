using SharedHome.Shared.Extensionss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public Response()
        {
            Data = default!;
            Message = default!;
        }

        public Response(T data, string message)
        {
            Data = data;
            Message = message;
        }

        public static Response<T> Ok(T data, string message) => new(data, message);
    }

    public static class Response
    {
        public static Response<string> Added(string entity) => new(default!, GetMessage(entity, ResponseType.Added));

        public static Response<string> Deleted(string entity) => new(default!, GetMessage(entity, ResponseType.Deleted));

        public static Response<string> Updated(string entity) => new(default!, GetMessage(entity, ResponseType.Updated));

        public static Response<string> Ok(string message) => new(default!, message);

        private static string GetMessage(string nameofEntity, ResponseType responseType)
        {
            nameofEntity = nameofEntity.SplitByUpperCase();

            return responseType switch
            {
                ResponseType.Added => $"{nameofEntity} added successfully.",

                ResponseType.Deleted => $"{nameofEntity} removed successfully.",

                ResponseType.Updated => $"{nameofEntity} updated successfully.",

                _ => ""
            };
        }
    }

}
