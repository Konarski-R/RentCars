using RentCars.Services;
using System.Data.Common;

namespace RentCars.Test
{
    public class InsertCarIntoDBTest
    {
        [Fact]
        public void Test()
        {
            // Arrange
            CarDBUsage cdb = new CarDBUsage();
            cdb.CreateFile();
            cdb.CreateCarTable();
            int amountofcars = cdb.SelectFromCarTable().Count;

            // Act
            cdb.InsertIntoCarTable("test", "", 00, 0, true, 0.0, "", 0.0);

            // Assert
            Assert.Equal(amountofcars + 1, cdb.SelectFromCarTable().Count);
        }
    }
}