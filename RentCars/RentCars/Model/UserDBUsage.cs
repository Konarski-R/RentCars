using Microsoft.Data.Sqlite;

namespace RentCars.Model

{
    public class UserDBUsage
    {
        public SqliteConnection connection;

        public void CreateFile()
        {
            string databaseFileName = AppContext.BaseDirectory + @"\PasswordManager.db";
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
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Users([UID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [Firstname] NVARCHAR(64) NOT NULL,[Lastname] NVARCHAR(64) NOT NULL, [Email] NVARCHAR(64) NOT NULL, [Password] NVARCHAR(64) NOT NULL, [Phone] NVARCHAR(64) NOT NULL, [Address] NVARCHAR(64) NOT NULL, [ZIP] INTEGER NOT NULL, [CITY] NVARCHAR(64) NOT NULL, s)";
                command.ExecuteNonQuery();
            }
        }

        public void InsertIntoUserTable(string InputUsername, string key)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                // Insert a record    
                command.CommandText = @"INSERT INTO Users(Username, Key) VALUES(@Username, @Key) ";
                command.Parameters.AddWithValue("@Username", InputUsername);
                command.Parameters.AddWithValue("@Key", key);
                command.ExecuteNonQuery();

            }
        }

        public User SelectFromUserTable(string userName)
        {
            User user = new User();
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id, UserName, Key from Users where UserName = '" + userName + "';";
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    Console.WriteLine(string.Format("Id: {0}",
                        result.GetString(0)));
                    Console.WriteLine(string.Format("UserName: {0}",
                        result.GetString(1)));
                    Console.WriteLine(string.Format("Key: {0}",
                        result.GetString(2)));

                    user.Id = result.GetInt32(0);
                    user.Username = result.GetString(1);
                    user.Key = result.GetString(2);
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
                command.CommandText = "SELECT * From Users WHERE Id = @UserId;";

                command.Parameters.AddWithValue("@UserId", userId);

                var result = command.ExecuteReader();
                if (result.Read())
                {
                    var user = new User
                    {
                        Id = result.GetInt32(0),
                        Username = result.GetString(1),
                        Key = result.GetString(2),
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
