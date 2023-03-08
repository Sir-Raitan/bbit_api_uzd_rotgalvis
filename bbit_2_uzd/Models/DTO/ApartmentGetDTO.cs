namespace bbit_2_uzd.Models.DTO
{
    public class ApartmentGetDTO
    {
        //izmantots get un get (id)
        public int Number { get; set; }

        public int Floor { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfTenants { get; set; }
        public double FullArea { get; set; }
        public double LivingArea { get; set; }
        public virtual HouseDTO House { get; set; }
    }
}