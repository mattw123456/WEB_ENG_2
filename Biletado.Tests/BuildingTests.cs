using System.Globalization;using System.Xml.Linq;using Biletado.Domain.Building;using Biletado.Domain.Storey;using Moq;namespace Biletado.Tests{    public class BuildingTests    {        private Mock<IBuildingService> _buildingServiceMock;        public BuildingTests()        {            _buildingServiceMock = new Mock<IBuildingService>();        }        [Fact]        public async Task CreateNewAsync_ShouldReturnStorey_WhenCreationSucceeds()
        {

            // Arrange
            var buildingId = Guid.NewGuid();            string name = "building1";            string streetname = "kingstreet";            String housenumber = "1";            string country_code = "us";            string postal_code = "12345";            string city = "kingcity";            var newBuilding = new Building
            {                 id = buildingId,                 name = name,                 streetname = streetname,                 housenumber = housenumber,                 country_code = country_code,                 postalcode = postal_code,                 city = city,                
            };            _buildingServiceMock.Setup(service => service.CreateBuildingAsync(newBuilding)).ReturnsAsync(newBuilding);

            // Act
            var result = await _buildingServiceMock.Object.CreateBuildingAsync(newBuilding);

            // Assert
            Assert.Equal(newBuilding, result);        }
        [Fact]        public async Task GetAll_ShouldReturnBuilding_WhenCreationSucceeds()
        {

            // Arrange
            var buildingId = Guid.NewGuid();            string name = "building1";            string streetname = "kingstreet";            String housenumber = "1";            string country_code = "us";            string postal_code = "12345";            string city = "kingcity";            var newBuilding = new Building
            {
                id = buildingId,
                name = name,
                streetname = streetname,
                housenumber = housenumber,
                country_code = country_code,
                postalcode = postal_code,
                city = city,

            };            _buildingServiceMock.Setup(service => service.CreateBuildingAsync(newBuilding)).ReturnsAsync(newBuilding);

            // Act
            var result = await _buildingServiceMock.Object.CreateBuildingAsync(newBuilding);

            // Assert
            Assert.Equal(newBuilding, result);        }



    }}