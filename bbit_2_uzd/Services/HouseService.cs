using bbit_2_uzd.Models;
using bbit_2_uzd.Services.Communication;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace bbit_2_uzd.Services
{
    public class HouseService : IHouseService
    {
        private readonly AppDatabaseConfig _context;

        public HouseService(AppDatabaseConfig context)
        {
            _context = context;
        }

        public async Task<IEnumerable<House>> GetAllHouses()
        {
            return await _context.Houses.ToListAsync();
        }
        public async Task<IEnumerable<House>> GetTenantHouses(Guid tenantId)
        {
            Tenant tenant = await _context.Tenants.Include(t => t.TenantApartments).FirstOrDefaultAsync(t => t.Id == tenantId);

            if (tenant != null)
            {
                var houses = tenant.TenantApartments.Select(a => a.House);
                return houses.ToList();
            }
            return new List<House>();
        }
        public async Task<HouseResponse> GetHouse(Guid id)
        {
            if (_context.Houses == null)
            {
                return new HouseResponse("No house data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var house = await _context.Houses.FindAsync(id);

            if (house == null)
            {
                return new HouseResponse($"House with the ID :{id} was not found", HttpStatusCode.NotFound);
            }

            return new HouseResponse(house);
        }

        public async Task<HouseResponse> PostHouse(House house)
        {
            if (_context.Houses == null)
            {
                return new HouseResponse("No house data found - table nonexistent", HttpStatusCode.NotFound);
            }
            try
            {
                await _context.Houses.AddAsync(house);
                _context.SaveChanges();

                return new HouseResponse(house);
            }
            catch (Exception ex)
            {
                return new HouseResponse($"There was a probelm while saving house data: {ex.Message}");
            }
        }

        public async Task<HouseResponse> PutHouse(Guid id, House house)
        {
            if (_context.Houses == null)
            {
                return new HouseResponse("No house data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var existingHouse = await _context.Houses.FindAsync(id);

            if (existingHouse == null)
            {
                return new HouseResponse("Cannot update information - matching Id not found", HttpStatusCode.NotFound);
            }
            existingHouse.Number = house.Number;
            existingHouse.Street = house.Street;
            existingHouse.City = house.City;
            existingHouse.Country = house.Country;
            existingHouse.PostalCode = house.PostalCode;
            _context.Entry(existingHouse).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                return new HouseResponse(existingHouse);
            }
            catch (Exception ex)
            {
                return new HouseResponse($"There was a problem while updating house information: {ex.Message}");
            }
        }

        public async Task<HouseResponse> DeleteHouse(Guid id)
        {
            if (_context.Houses == null)
            {
                return new HouseResponse("No house data found - table nonexistent", HttpStatusCode.NotFound);
            }

            var house = await _context.Houses.FindAsync(id);
            if (house == null)
            {
                return new HouseResponse($"House with the id :{id} cannot be found.", HttpStatusCode.NotFound);
            }

            try
            {
                _context.Houses.Remove(house);
                _context.SaveChanges();

                return new HouseResponse(house);
            }
            catch (Exception ex)
            {
                return new HouseResponse($"There was a problem removing the house: {ex.Message}");
            }
        }
    }
}