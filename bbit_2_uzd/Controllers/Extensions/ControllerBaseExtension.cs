using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bbit_2_uzd.Controllers.Extensions
{
    public static class ControllerBaseExtension
    {
        public static ActionResult ResolveError(this ControllerBase controller, HttpStatusCode code, string message)
        {
            switch (code)
            {
                case HttpStatusCode.NoContent:
                    return controller.NoContent();

                case HttpStatusCode.BadRequest:
                    return controller.BadRequest(message);

                case HttpStatusCode.Unauthorized:
                    return controller.Unauthorized(message);

                case HttpStatusCode.Forbidden:
                    return controller.Forbid();

                case HttpStatusCode.NotFound:
                    return controller.NotFound(message);

                default:
                    return controller.BadRequest(message);
            }
        }
    }
}