using RentCars.Model;

namespace RentCars.Services
{
    public class DataService
    {
        public int SharedUserID { get; set; }
        public string Plan { get; set; }
        public List<Addition> Additions { get; set; }
        public int Days { get; set; }
        public string Paymentmethod { get; set; }

        public DataService() 
        { 
            Additions = new List<Addition>();
        }

        public void ClearRentData()
        {
            Additions.Clear();
            Days = 0;
            Plan = null;
            Paymentmethod = null;
        }
    }
}
