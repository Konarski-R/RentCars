using Microsoft.Data.Sqlite;
using RentCars.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace RentCars.Services
{
    public class CarDBUsage
    {
        private SqliteConnection connection;
        private readonly string databaseFileName;

        public CarDBUsage()
        {
            databaseFileName = Path.Combine(AppContext.BaseDirectory, "CarRent.db");
        }

        public void CreateFile()
        {
            if (!File.Exists(databaseFileName))
            {
                var myFile = File.Create(databaseFileName);
                myFile.Close();
            }
        }

        private void OpenConnection()
        {
            if (connection == null)
            {
                connection = new SqliteConnection("Data Source=" + databaseFileName);
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void CreateCarTable()
        {
            try
            {
                OpenConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Car(
                                                CID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                CarName NVARCHAR(64) NOT NULL,
                                                CarBrand NVARCHAR(64) NOT NULL,
                                                Seats INTEGER NOT NULL,
                                                Doors INTEGER NOT NULL,
                                                IsAutomatic INTEGER NOT NULL,
                                                HorsePwr REAL NOT NULL,
                                                CarPic NVARCHAR(100) NOT NULL,
                                                CarPrice REAL NOT NULL)";
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public void InsertIntoCarTable(string carName, string carBrand, int seats, int doors, bool isAutomatic, double horsePwr, string carpicture, double carprice)
        {
            try
            {
                OpenConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Car(CarName, CarBrand, Seats, Doors, IsAutomatic, HorsePwr, CarPic, CarPrice) 
                                            VALUES(@CarName, @CarBrand, @Seats, @Doors, @IsAutomatic, @HorsePwr, @CarPic, @CarPrice)";
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
            finally
            {
                CloseConnection();
            }
        }

        public Car SelectCarFromId(int id)
        {
            try
            {
                OpenConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT CID, CarName, CarBrand, Seats, Doors, IsAutomatic, HorsePwr, CarPic, CarPrice FROM Car WHERE CID = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            return new Car(
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
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }

        public List<Car> SelectFromCarTable()
        {
            List<Car> carList = new List<Car>();
            try
            {
                OpenConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Car";
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            carList.Add(new Car(
                                result.GetInt32(0),
                                result.GetString(1),
                                result.GetString(2),
                                result.GetInt32(3),
                                result.GetInt32(4),
                                result.GetInt32(5) == 1,
                                result.GetDouble(6),
                                result.GetString(7),
                                result.GetDouble(8)
                            ));
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return carList;
        }

        public void DeleteFromCarTable(int carID)
        {
            try
            {
                OpenConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Car WHERE CID = @CarID";
                    command.Parameters.AddWithValue("@CarID", carID);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public void EditCar(string changedColumn, string changedValue, int carID)
        {
            try
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
            finally
            {
                CloseConnection();
            }
        }
    }
}
