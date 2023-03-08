using bbit_2_uzd.Models;
using bbit_2_uzd.Services.Communication;

namespace bbit_2_uzd.Services.Interfaces
{
    public interface IHouseService
    {
        public Task<IEnumerable<House>> GetAllHouses();

        public Task<HouseResponse> GetHouse(Guid id);

        public Task<HouseResponse> PutHouse(Guid id, House house);

        public Task<HouseResponse> PostHouse(House house);

        public Task<HouseResponse> DeleteHouse(Guid id);
    }
}