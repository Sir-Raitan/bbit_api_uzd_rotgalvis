﻿using bbit_2_uzd.Models;
using bbit_2_uzd.Services.Communication;

namespace bbit_2_uzd.Services.Interfaces
{
    public interface IApartmentService
    {
        public Task<IEnumerable<Apartment>> GetAllApartments();

        public Task<IEnumerable<Apartment>> GetApartmentsFromHouse(Guid id);

        public Task<IEnumerable<Apartment>> GetTenantApartmentsFromHouse(Guid houseID, Guid tenantID);

        public Task<ApartmentResponse> GetApartment(Guid id);

        public Task<ApartmentResponse> PutApartment(Guid id, Apartment dzivoklis);

        public Task<ApartmentResponse> PostApartment(Apartment dzivoklis);

        public Task<ApartmentResponse> DeleteApartment(Guid id);
    }
}