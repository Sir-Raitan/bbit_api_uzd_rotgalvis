using bbit_2_uzd.Models;
using bbit_2_uzd.Services.Communication;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace bbit_2_uzd.Services
{
    public class TenantService : ITenantService
    {
        private readonly AppDatabaseConfig _context;

        public TenantService(AppDatabaseConfig context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tenant>> GetAllTenants()
        {
            return await _context.Tenants.Include(t => t.TenantApartments).ToListAsync();
        }
        public async Task<IEnumerable<Tenant>> GetTenantsWithApartment(Guid id)
        {
            return await _context.Tenants.Include(t => t.TenantApartments).Where(t => t.TenantApartments.Any(a => a.Id == id)).ToListAsync();
        }

        public async Task<TenantResponse> GetTenant(Guid id)
        {
            if (_context.Tenants == null)
            {
                return new TenantResponse("No tenant data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var tenant = await _context.Tenants.Include(t => t.TenantApartments).FirstOrDefaultAsync(t => t.Id == id);

            if (tenant == null)
            {
                return new TenantResponse($"House with the ID :{id} was not found", HttpStatusCode.NotFound);
            }

            return new TenantResponse(tenant);
        }

        public async Task<TenantResponse> PostTenant(Tenant tenant)
        {
            if (_context.Tenants == null)
            {
                return new TenantResponse("No tenant data found - table nonexistent", HttpStatusCode.NotFound);
            }
            try
            {
                await _context.Tenants.AddAsync(tenant);
                foreach (Apartment d in tenant.TenantApartments)
                {
                    d.NumberOfTenants++;
                    _context.Entry(d).State = EntityState.Modified;
                }

                _context.SaveChanges();

                var tenantFinal = await _context.Tenants.Include(t => t.TenantApartments).FirstOrDefaultAsync(t => t.Id == tenant.Id);

                if (tenantFinal != null)
                {
                    return new TenantResponse(tenantFinal);
                }

                return new TenantResponse(tenant);
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return new TenantResponse($"There was a probelm while saving tenant data: {message}");
            }
        }

        public async Task<TenantResponse> PutTenant(Guid id, Tenant tenant)
        {
            if (_context.Tenants == null)
            {
                return new TenantResponse("No house data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var existingTenant = await _context.Tenants.Include(x => x.TenantApartments).FirstOrDefaultAsync(t => t.Id == id);

            if (existingTenant == null)
            {
                return new TenantResponse("Cannot update information - matching Id not found", HttpStatusCode.NotFound);
            }

            existingTenant.Name = tenant.Name;
            existingTenant.Surname = tenant.Surname;
            existingTenant.PersonalCode = tenant.PersonalCode;
            existingTenant.DateOfBirth = tenant.DateOfBirth;
            existingTenant.Email = tenant.Email;
            existingTenant.IsOwner = tenant.IsOwner;
            existingTenant.PhoneNumber = tenant.PhoneNumber;

            var apartmentsAdded = tenant.TenantApartments.Where(dz => !existingTenant.TenantApartments.Any(dz2 => dz2.Id == dz.Id));
            foreach (Apartment d in apartmentsAdded)
            {
                d.NumberOfTenants++;
            }

            var apartmentsRemoved = existingTenant.TenantApartments.Where(dz => !tenant.TenantApartments.Any(dz2 => dz2.Id == dz.Id));
            foreach (Apartment d in apartmentsRemoved)
            {
                d.NumberOfTenants--;
            }

            existingTenant.TenantApartments.Clear();
            existingTenant.TenantApartments.AddRange(tenant.TenantApartments);

            _context.Entry(existingTenant).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                var tenantFinal = await _context.Tenants.Include(t => t.TenantApartments).FirstOrDefaultAsync(t => t.Id == id);

                if (tenantFinal != null)
                {
                    return new TenantResponse(tenantFinal);
                }

                return new TenantResponse(existingTenant);
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return new TenantResponse($"There was a problem while updating tenant information: {message}");
            }
        }

        public async Task<TenantResponse> DeleteTenant(Guid id)
        {
            if (_context.Tenants == null)
            {
                return new TenantResponse("No tenant data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var tenant = await _context.Tenants.Include(t => t.TenantApartments).FirstOrDefaultAsync(x => x.Id == id);
            if (tenant == null)
            {
                return new TenantResponse($"Tenant with the id :{id} cannot be found.", HttpStatusCode.NotFound);
            }

            try
            {
                foreach (Apartment d in tenant.TenantApartments)
                {
                    d.NumberOfTenants--;
                    _context.Entry(d).State = EntityState.Modified;
                }

                _context.Tenants.Remove(tenant);
                _context.SaveChanges();

                return new TenantResponse(tenant);
            }
            catch (Exception e) 
            { 
                string message = e.InnerException == null ? e.Message : e.InnerException.Message; 
                return new TenantResponse($"There was a problem removing the tenant: {message}"); 
            }
        }
    }
}