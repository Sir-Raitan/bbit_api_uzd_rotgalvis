using AutoMapper;
using bbit_2_uzd.Models;
using bbit_2_uzd.Models.DTO;

namespace bbit_2_uzd.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<House, HouseDTO>();
            CreateMap<Apartment, ApartmentModifyDTO>();
            CreateMap<Apartment, ApartmentGetDTO>();
            CreateMap<Tenant, TenantModifyDTO>();
            CreateMap<Tenant, TenantGetDTO>();
        }
    }
}