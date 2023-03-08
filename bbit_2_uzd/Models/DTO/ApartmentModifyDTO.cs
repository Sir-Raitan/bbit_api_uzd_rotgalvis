namespace bbit_2_uzd.Models.DTO
{
    public class ApartmentModifyDTO
    {
        //izmantots put un post
        public int Number { get; set; }

        public int Floor { get; set; }
        public int NumberOfRooms { get; set; }
        public double FullArea { get; set; }
        public double LivingArea { get; set; }
        public Guid HouseId { get; set; }
    }
}