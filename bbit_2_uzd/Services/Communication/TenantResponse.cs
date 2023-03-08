using bbit_2_uzd.Models;
using System.Net;

namespace bbit_2_uzd.Services.Communication
{
    public class TenantResponse : ServiceResponse<Tenant>
    {
        public TenantResponse(Tenant tenant) : base(tenant)
        {
        }

        public TenantResponse(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message, code)
        {
        }
    }
}