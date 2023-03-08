using AutoMapper;
using bbit_2_uzd.Models;

namespace bbit_2_uzd.Mapping.Helpers
{
    public class IdToApartmentConverter : ITypeConverter<Guid, Apartment>
    {
        private AppDatabaseConfig _context;

        public IdToApartmentConverter(AppDatabaseConfig context)
        {
            _context = context;
        }

        public Apartment Convert(Guid source, Apartment destination, ResolutionContext context)
        {
            return GetApartmnet(source);
        }

        private Apartment GetApartmnet(Guid id)
        {
            var apartmanent = _context.Apartments.First(a => a.Id == id);

            if (apartmanent == null)
            {
                throw new Exception($"Apartment with the id:{id} does not exist");
            }

            return apartmanent;
        }
    }
}