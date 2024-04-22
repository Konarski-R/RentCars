namespace RentCars.Model
{
    public class Car
    {
        public int CID { get; set; }
        public string CarName { get; set; }
        public string CarBrand { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public bool IsAutomatic { get; set; }
        public double HorsePwr { get; set; }

    }
}
