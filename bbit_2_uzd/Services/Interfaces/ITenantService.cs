using bbit_2_uzd.Models;
using bbit_2_uzd.Services.Communication;

namespace bbit_2_uzd.Services.Interfaces
{
    public interface ITenantService
    {
        public Task<IEnumerable<Tenant>> GetAllTenants();

        public Task<TenantResponse> GetTenant(Guid id);

        public Task<TenantResponse> PutTenant(Guid id, Tenant tenant);

        public Task<TenantResponse> PostTenant(Tenant tenant);

        public Task<TenantResponse> DeleteTenant(Guid id);
    }
}