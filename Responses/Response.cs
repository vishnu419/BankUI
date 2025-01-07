using System.Net;

namespace BankUI.Responses
{
    public class Response<T>
    {
        public T? Data { get; set; }

        public List<Error>? Errors { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }

    public class Error

    {

        public string? Code { get; set; }

        public string? Message { get; set; }

        public string? Field { get; set; }

    }
}
