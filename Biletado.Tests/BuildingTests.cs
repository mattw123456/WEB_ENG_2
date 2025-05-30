using System.Globalization;
using System.Xml.Linq;
using Biletado.Domain.Building;
using Biletado.Domain.Storey;
using Moq;

namespace Biletado.Tests
{
    public class BuildingTests
    {
        private Mock<IBuildingService> _buildingServiceMock;

        public BuildingTests()
        {
            _buildingServiceMock = new Mock<IBuildingService>();
        }

        [Fact]
        public async Task CreateNewAsync_ShouldReturnStorey_WhenCreationSucceeds()
        {

            // Arrange
            var buildingId = Guid.NewGuid();
            string name = "building1";
            string streetname = "kingstreet";
            String housenumber = "1";
            string country_code = "us";
            string postal_code = "12345";
            string city = "kingcity";

            var newBuilding = new Building
            {
                 id = buildingId,
                 name = name,
                 streetname = streetname,
                 housenumber = housenumber,
                 country_code = country_code,
                 postalcode = postal_code,
                 city = city,
                
            };

            _buildingServiceMock.Setup(service => service.CreateBuildingAsync(newBuilding)).ReturnsAsync(newBuilding);

            // Act
            var result = await _buildingServiceMock.Object.CreateBuildingAsync(newBuilding);

            // Assert
            Assert.Equal(newBuilding, result);
        }
        [Fact]
        public async Task GetAll_ShouldReturnBuilding_WhenCreationSucceeds()
        {

            // Arrange
            var buildingId = Guid.NewGuid();
            string name = "building1";
            string streetname = "kingstreet";
            String housenumber = "1";
            string country_code = "us";
            string postal_code = "12345";
            string city = "kingcity";

            var newBuilding = new Building
            {
                id = buildingId,
                name = name,
                streetname = streetname,
                housenumber = housenumber,
                country_code = country_code,
                postalcode = postal_code,
                city = city,

            };

            _buildingServiceMock.Setup(service => service.CreateBuildingAsync(newBuilding)).ReturnsAsync(newBuilding);

            // Act
            var result = await _buildingServiceMock.Object.CreateBuildingAsync(newBuilding);

            // Assert
            Assert.Equal(newBuilding, result);
        }
        [Fact]
        public async Task GetBuildingByIdAsync_ShouldReturnBuilding_WhenBuildingExists()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            var expectedBuilding = new Building
            {
                id = buildingId,
                name = "Test Building",
                streetname = "Main St",
                housenumber = "42",
                postalcode = "12345",
                city = "Test City",
                country_code = "DE"
            };

            _buildingServiceMock
                .Setup(service => service.GsetBuildingByIdAsync(buildingId))
                .ReturnsAsync(expectedBuilding);

            // Act
            var result = await _buildingServiceMock.Object.GetBuildingByIdAsync(buildingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBuilding.id, result.id);
            Assert.Equal(expectedBuilding.name, result.name);
        }
        [Fact]
        public async Task DeleteBuildingAsync_ShouldReturnFalse_WhenBuildingDoesNotExist()
        {
            // Arrange
            var buildingId = Guid.NewGuid();

            _buildingServiceMock
                .Setup(service => service.DeleteBuildingAsync(buildingId, false))
                .ReturnsAsync((false, "Building not found"));

            // Act
            var result = await _buildingServiceMock.Object.DeleteBuildingAsync(buildingId, false);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Building not found", result.ErrorMessage);
        }





    }
}