using bbit_2_uzd.Models;
using bbit_2_uzd.Services.Communication;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace bbit_2_uzd.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly AppDatabaseConfig _context;

        public ApartmentService(AppDatabaseConfig context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartment>> GetAllApartments()
        {
            return await _context.Apartments.Include(a => a.House).ToListAsync();
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsFromHouse(Guid id)
        {
            return await _context.Apartments.Where(a => a.HouseId == id).ToListAsync();
        }

        public async Task<IEnumerable<Apartment>> GetTenantApartmentsFromHouse(Guid houseId, Guid tenantId)
        {
            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId);
            var apartments = tenant.TenantApartments.Where(a => a.HouseId == houseId).ToList();
            return apartments;
        }
        public async Task<ApartmentResponse> GetApartment(Guid id)
        {
            if (_context.Apartments == null)
            {
                return new ApartmentResponse("No apartment data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var apartment = await _context.Apartments.Include(a => a.House).FirstOrDefaultAsync(d => d.Id == id);

            if (apartment == null)
            {
                return new ApartmentResponse($"Apartment with the ID :{id} was not found", HttpStatusCode.NotFound);
            }

            return new ApartmentResponse(apartment);
        }

        public async Task<ApartmentResponse> PostApartment(Apartment apartment)
        {
            if (_context.Apartments == null)
            {
                return new ApartmentResponse("No apartment data found - table nonexistent", HttpStatusCode.NotFound);
            }
            try
            {
                await _context.Apartments.AddAsync(apartment);

                _context.SaveChanges();

                var apartmentFull = await _context.Apartments.Include(a => a.House).FirstOrDefaultAsync(a => a.Id == apartment.Id);

                if (apartmentFull != null)
                {
                    return new ApartmentResponse(apartmentFull);
                }

                return new ApartmentResponse(apartment);
            }
            catch (Exception e) 
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return new ApartmentResponse($"There was a probelm while saving apartment data: {message}"); 
            }
        }

        public async Task<ApartmentResponse> PutApartment(Guid id, Apartment apartment)
        {
            if (_context.Apartments == null)
            {
                return new ApartmentResponse("No apartment data found - table nonexistent", HttpStatusCode.NotFound);
            }
            var existingApartment = await _context.Apartments.FindAsync(id);

            if (existingApartment == null)
            {
                return new ApartmentResponse("Cannot update information - matching Id not found", HttpStatusCode.NotFound);
            }

            existingApartment.Number = apartment.Number;
            existingApartment.Floor = apartment.Floor;
            existingApartment.FullArea = apartment.FullArea;
            existingApartment.LivingArea = apartment.LivingArea;
            existingApartment.HouseId = apartment.HouseId;
            _context.Entry(existingApartment).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                var apartmentFull = await _context.Apartments.Include(a => a.House).FirstOrDefaultAsync(a => a.Id == id);

                if (apartmentFull != null)
                {
                    return new ApartmentResponse(apartmentFull);
                }

                return new ApartmentResponse(existingApartment);
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return new ApartmentResponse($"There was a problem while updating apartment information: {message}");
            }
        }

        public async Task<ApartmentResponse> DeleteApartment(Guid id)
        {
            if (_context.Apartments == null)
            {
                return new ApartmentResponse("No apartment data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return new ApartmentResponse($"Apartment with the id :{id} cannot be found.", HttpStatusCode.NotFound);
            }

            try
            {
                _context.Apartments.Remove(apartment);
                _context.SaveChanges();

                return new ApartmentResponse(apartment);
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return new ApartmentResponse($"There was a problem removing the apartment: {message}");
            }
        }
    }
}