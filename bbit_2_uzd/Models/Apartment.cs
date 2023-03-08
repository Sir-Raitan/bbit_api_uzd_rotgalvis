namespace bbit_2_uzd.Models
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfTenants { get; set; }
        public decimal FullArea { get; set; }
        public decimal LivingArea { get; set; }
        public Guid HouseId { get; set; }
        public virtual House House { get; set; }
    }
}