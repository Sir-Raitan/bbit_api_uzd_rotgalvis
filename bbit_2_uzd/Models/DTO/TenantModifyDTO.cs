namespace bbit_2_uzd.Models.DTO
{
    public class TenantModifyDTO
    {
        //prieks put un post
        public string Name { get; set; }

        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsOwner { get; set; }
        public List<Guid> ApartmnetsId { get; set; }
    }
}