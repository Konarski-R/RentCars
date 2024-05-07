using Microsoft.Data.Sqlite;

namespace RentCars.Model

{
    public class UserDBUsage
    {
        public SqliteConnection connection;

        public void CreateFile()
        {
            string databaseFileName = AppContext.BaseDirectory + @"\CarRent.db";
            if (!File.Exists(databaseFileName))
            {
                var myFile = File.Create(databaseFileName);
                myFile.Close();
            }
        }

        public void OpenConnection()
        {
            string databaseFileName = AppContext.BaseDirectory + @"\CarRent.db";
            connection = new SqliteConnection("Data Source=" + databaseFileName);
            connection.Open();
        }


        public void CreateUserTable()
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                // Create table if not exist    
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Users([UID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [FIRSTNAME] NVARCHAR(64) NOT NULL,[LASTNAME] NVARCHAR(64) NOT NULL, [EMAIL] NVARCHAR(64) NOT NULL, [PASSWORD] NVARCHAR(64) NOT NULL, [PHONE] NVARCHAR(64) NOT NULL, [ADDRESS] NVARCHAR(64) NOT NULL, [ZIP] INTEGER NOT NULL, [CITY] NVARCHAR(64) NOT NULL, [COUNTRY] NVARCHAR(64) NOT NULL, [BIRTHDATE] DATE NOT NULL, [BIRTHCITY] NVARCHAR(64) NOT NULL, [BIRTHCOUNTRY] NVARCHAR(64) NOT NULL, [DLNUMBER] INTEGER NOT NULL, [DLISSUEDATE] DATE NOT NULL, [DLEXPIRYDATE] DATE NOT NULL, [DLISSUECITY] NVARCHAR(64) NOT NULL, [DLISSUECOUNTRY] NVARCHAR(64) NOT NULL, [IDNUMBER] INTEGER NOT NULL, [PASSPORTNUMBER] INTEGER NOT NULL, [PASSPORTEXPIRYDATE] DATE NOT NULL, [PASSPORTISSUEDATE] DATE NOT NULL, [PASSPORTISSUECITY] NVARCHAR(64) NOT NULL, [PASSPORTISSSUECOUNTRY] NVARCHAR(64) NOT NULL)";
                command.ExecuteNonQuery();
            }
        }

        public void InsertIntoUserTable(string firstName, string lastName, string email, string password, string phone, string address, int zip, string city, string country, DateTime birthDate, string birthCity, string birthCountry, int dlNumber, DateTime dlIssueDate, DateTime dlExpiryDate, string dlIssueCity, string dlIssueCountry, int idNumber, int passportNumber, DateTime passportExpiryDate, DateTime passportIssueDate, string passportIssueCity, string passportIssueCountry)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                // Insert a record    
                command.CommandText = @"INSERT INTO Users(FIRSTNAME, LASTNAME, EMAIL, PASSWORD, PHONE, ADDRESS, ZIP, CITY, COUNTRY, BIRTHDATE, BIRTHCITY, BIRTHCOUNTRY, DLNUMBER, DLISSUEDATE, DLEXPIRYDATE, DLISSUECITY, DLISSUECOUNTRY, IDNUMBER, PASSPORTNUMBER, PASSPORTEXPIRYDATE, PASSPORTISSUEDATE, PASSPORTISSUECITY, PASSPORTISSSUECOUNTRY) VALUES(@FirstName, @LastName, @Email, @Password, @Phone, @Address, @Zip, @City, @Country, @BirthDate, @BirthCity, @BirthCountry, @DLNumber, @DLIssueDate, @DLExpiryDate, @DLIssueCity, @DLIssueCountry, @IDNumber, @PassportNumber, @PassportExpiryDate, @PassportIssueDate, @PassportIssueCity, @PassportIssueCountry)";

                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Zip", zip);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@Country", country);
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@BirthCity", birthCity);
                command.Parameters.AddWithValue("@BirthCountry", birthCountry);
                command.Parameters.AddWithValue("@DLNumber", dlNumber);
                command.Parameters.AddWithValue("@DLIssueDate", dlIssueDate);
                command.Parameters.AddWithValue("@DLExpiryDate", dlExpiryDate);
                command.Parameters.AddWithValue("@DLIssueCity", dlIssueCity);
                command.Parameters.AddWithValue("@DLIssueCountry", dlIssueCountry);
                command.Parameters.AddWithValue("@IDNumber", idNumber);
                command.Parameters.AddWithValue("@PassportNumber", passportNumber);
                command.Parameters.AddWithValue("@PassportExpiryDate", passportExpiryDate);
                command.Parameters.AddWithValue("@PassportIssueDate", passportIssueDate);
                command.Parameters.AddWithValue("@PassportIssueCity", passportIssueCity);
                command.Parameters.AddWithValue("@PassportIssueCountry", passportIssueCountry);

                command.ExecuteNonQuery();
            }
        }

        public User SelectFromUserTable(string email)
        {
            User user = new User();
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT UID, EMAIL, PASSWORD from Users where EMAIL = '" + email + "';";
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    Console.WriteLine(string.Format("UID: {0}",
                        result.GetString(0)));
                    Console.WriteLine(string.Format("EMAIL: {0}",
                        result.GetString(1)));
                    Console.WriteLine(string.Format("PASSWORD: {0}",
                        result.GetString(2)));

                    user.UID = result.GetInt32(0);
                    user.Email = result.GetString(1);
                    user.Password = result.GetString(2);
                    return user;
                }
            }
            return null;
        }

        public User UserGetUserdById(int userId)
        {
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * From Users WHERE UID = @UserId;";

                command.Parameters.AddWithValue("@UserId", userId);

                var result = command.ExecuteReader();
                if (result.Read())
                {
                    var user = new User
                    {
                        UID = result.GetInt32(0),
                        Firstname = result.GetString(1),
                        Lastname = result.GetString(2),
                        Email = result.GetString(3),
                        Password = result.GetString(4),
                        Phone = result.GetString(5),
                        Address = result.GetString(6),
                        ZIP = result.GetInt32(7),
                        City = result.GetString(8),
                        Country = result.GetString(9),
                        Birthdate = result.GetDateTime(10),
                        BirthCity = result.GetString(11),
                        BirthCountry = result.GetString(12),
                        DLNumber = result.GetInt32(13),
                        DLIssueDate = result.GetDateTime(14),
                        DLExpiryDate = result.GetDateTime(15),
                        DLIssueCity = result.GetString(16),
                        DLIssueCountry = result.GetString(17),
                        IDNumber = result.GetInt32(18),
                        PassportNumber = result.GetInt32(19),
                        PassportExpiryDate = result.GetDateTime(20),
                        PassportIssueDate = result.GetDateTime(21),
                        PassportIssueCity = result.GetString(22),
                        PassportIssueCountry = result.GetString(23)
                    };
                    return user;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
