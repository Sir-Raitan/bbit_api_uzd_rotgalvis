using bbit_2_uzd.Models;
using System.Net;

namespace bbit_2_uzd.Services.Communication
{
    public class ApartmentResponse : ServiceResponse<Apartment>
    {
        public ApartmentResponse(Apartment dzivoklis) : base(dzivoklis)
        {
        }

        public ApartmentResponse(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message, code)
        {
        }
    }
}