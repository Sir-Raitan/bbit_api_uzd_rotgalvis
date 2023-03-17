namespace bbit_2_uzd.Models.DTO
{
    public class TenantGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsOwner { get; set; }
        public virtual List<ApartmentGetDTO> TenantApartments { get; set; } = new List<ApartmentGetDTO>();
    }
}