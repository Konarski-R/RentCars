using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace RentCars.Model
{
    public class CarDBUsage
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

        public void CreateCarTable()
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                // Create table if not exist    
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Car(CID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CarName NVARCHAR(64) NOT NULL, CarBrand NVARCHAR(64) NOT NULL, Seats INTEGER NOT NULL, Doors INTEGER NOT NULL, IsAutomatic INTEGER NOT NULL, HorsePwr REAL NOT NULL, CarPic NVARCHAR(100) NOT NULL, CarPrice REAL NOT NULL)";
                command.ExecuteNonQuery();
            }
        }

        public void InsertIntoCarTable(string carName, string carBrand, int seats, int doors, bool isAutomatic, double horsePwr, string carpicture, double carprice)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                // Insert a record    
                command.CommandText = @"INSERT INTO Car(CarName, CarBrand, Seats, Doors, IsAutomatic, HorsePwr, CarPic, CarPrice) VALUES(@CarName, @CarBrand, @Seats, @Doors, @IsAutomatic, @HorsePwr, @CarPic, @CarPrice) ";
                command.Parameters.AddWithValue("@CarName", carName);
                command.Parameters.AddWithValue("@CarBrand", carBrand);
                command.Parameters.AddWithValue("@Seats", seats);
                command.Parameters.AddWithValue("@Doors", doors);
                command.Parameters.AddWithValue("@IsAutomatic", isAutomatic ? 1 : 0);
                command.Parameters.AddWithValue("@HorsePwr", horsePwr);
                command.Parameters.AddWithValue("@CarPic", carpicture);
                command.Parameters.AddWithValue("@CarPrice", carprice);
                command.ExecuteNonQuery();
            }
        }

        public Car SelectCarFromId(int id)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT CID, CarName, CarBrand, Seats, Doors, IsAutomatic, HorsePwr, CarPic, CarPrice FROM Car WHERE CID = {id};";

                var result = command.ExecuteReader();
                
                while(result.Read())
                {
                    Car car = new Car(
                        result.GetInt32(0),
                        result.GetString(1),
                        result.GetString(2),
                        result.GetInt32(3),
                        result.GetInt32(4),
                        result.GetInt32(5) == 1,
                        result.GetDouble(6),
                        result.GetString(7),
                        result.GetDouble(8)
                    );
                    return car;
                }
            
            }
            return null;
        }

        public List<Car> SelectFromCarTable()
        {
            OpenConnection();
            List<Car> carList = new List<Car>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT CID, CarName, CarBrand, Seats, Doors, IsAutomatic, HorsePwr, CarPic, CarPrice FROM Car";
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    Car car = new Car(
                        result.GetInt32(0),
                        result.GetString(1),
                        result.GetString(2),
                        result.GetInt32(3),
                        result.GetInt32(4),
                        result.GetInt32(5) == 1,
                        result.GetDouble(6),
                        result.GetString(7),
                        result.GetDouble(8)
                    );
                    carList.Add(car);
                }
            }

            return carList;
        }

        public void DeleteFromCarTable(int carID)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Car WHERE CID = @CarID";
                command.Parameters.AddWithValue("@CarID", carID);
                command.ExecuteNonQuery();
            }
        }

        public void EditCar(string changedColumn, string changedValue, int carID)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE Car SET {changedColumn} = @ChangedValue WHERE CID = @CarID";
                command.Parameters.AddWithValue("@ChangedValue", changedValue);
                command.Parameters.AddWithValue("@CarID", carID);
                command.ExecuteNonQuery();
            }
        }
    }
}
