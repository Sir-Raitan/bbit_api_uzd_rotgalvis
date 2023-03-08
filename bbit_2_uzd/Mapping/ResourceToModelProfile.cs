using AutoMapper;
using bbit_2_uzd.Mapping.Helpers;
using bbit_2_uzd.Models;
using bbit_2_uzd.Models.DTO;

namespace bbit_2_uzd.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<Guid, Apartment>().ConvertUsing(typeof(IdToApartmentConverter));

            CreateMap<HouseDTO, House>();
            CreateMap<ApartmentModifyDTO, Apartment>();
            CreateMap<ApartmentGetDTO, Apartment>();
            CreateMap<TenantModifyDTO, Tenant>().ForMember(i => i.TenantApartments, opt => opt.MapFrom(src => src.ApartmnetsId));
            CreateMap<TenantGetDTO, Tenant>();
        }
    }
}