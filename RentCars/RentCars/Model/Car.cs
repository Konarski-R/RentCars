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
        public string CarPic { get; set; }
        public double CarPrice { get; set; }

        public Car(int id, string name, string brand, int seats, int doors, bool isautomatic, double horsepwr, string carPic, double carPrice)
        {
            CID = id;
            CarName = name;
            CarBrand = brand;
            Seats = seats;
            Doors = doors;
            IsAutomatic = isautomatic;
            HorsePwr = horsepwr;
            CarPic = carPic;
            CarPrice = carPrice;
        }
        public Car() { }
    }
}
