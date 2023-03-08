using bbit_2_uzd.Models;
using System.Net;

namespace bbit_2_uzd.Services.Communication
{
    public class HouseResponse : ServiceResponse<House>
    {
        public HouseResponse(House house) : base(house)
        {
        }

        public HouseResponse(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message, code)
        {
        }
    }
}