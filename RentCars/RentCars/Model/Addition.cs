namespace RentCars.Model
{
    public class Addition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public bool IsSelected { get; set; }

        public Addition(string name, string description, string imagepath, double price) 
        {
            Name = name;
            Description = description;
            ImagePath = imagepath;
            Price = price;
            IsSelected = false;
        }
    }
}
