namespace RentCars.Model
{
    public class Car
    {
        public long CID { get; set; }
        public string CarName { get; set; }
        public string CarBrand { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public bool IsAutomatic { get; set; }
        public double HorsePwr { get; set; }

        public Car(long id, string name, string brand)
        {
            CID = id;
            CarName = name;
            CarBrand = brand;
        }
    }
}
