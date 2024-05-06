namespace RentCars.Model
{
	public class User
	{
		public long UID { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public int ZIP { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public DateTime Birthdate { get; set; }
		public string BirthCity { get; set; }
		public string BirthCountry { get; set; }
		public long DLNumber { get; set; }
		public DateTime DLIssueDate { get; set; }
		public DateTime DLExpiryDate { get; set; }
		public string DLIssueCity { get; set; }
		public string DLIssueCountry { get; set; }
		public long IDNumber { get; set; }
		public long PassportNumber { get; set; }
		public DateTime PassportExpiryDate { get; set; }
		public DateTime PassportIssueDate { get; set; }
		public string PassportIssueCity { get; set; }
		public string PassportIssueCountry { get; set; }
	}
}
