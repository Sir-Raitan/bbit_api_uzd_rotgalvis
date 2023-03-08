using System.Net;

namespace bbit_2_uzd.Services.Communication
{
    public class ServiceResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public T Resource { get; private set; }

        public ServiceResponse(T resource, HttpStatusCode code = HttpStatusCode.OK)
        {
            Success = true;
            Message = string.Empty;
            StatusCode = code;
            Resource = resource;
        }

        public ServiceResponse(string message, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            Success = false;
            Message = message;
            Resource = default;
            StatusCode = code;
        }
    }
}