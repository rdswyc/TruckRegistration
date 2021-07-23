using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TruckRegistration.Domain;
using TruckRegistration.Models;
using Xunit;

namespace Tests
{
    public class TruckServiceTest
    {
        [Fact]
        public void Add_Success_ShouldAddAndReturnTruck()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            var entity = new TruckCreator(4, "FM", 2022, 2021);
            mockRepos.Setup(m => m.Add(It.IsAny<Truck>())).Returns(entity);
            var service = new TruckService(logger, mockRepos.Object);
            var model = new TruckViewModel
            {
                Model = "FH",
                ProductionYear = 2021,
                ModelYear = 2022
            };

            // Act
            var result = service.Add(model);

            // Assert
            Assert.Equal(4, result.Id);
            mockRepos.Verify(m => m.Add(It.IsAny<Truck>()), Times.Once());
        }

        [Fact]
        public void Add_DatabaseError_ShouldThrow400()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Add(It.IsAny<Truck>())).Throws(new Exception());
            var service = new TruckService(logger, mockRepos.Object);
            var model = new TruckViewModel();

            // Act
            void action() => service.Add(model);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Add(It.IsAny<Truck>()), Times.Once());
        }

        [Fact]
        public void Delete_Success_ShouldRemoveTruck()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Delete(It.IsAny<int>()));
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(true);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            service.Delete(2);

            // Assert
            mockRepos.Verify(m => m.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Delete_DoesNotExist_ShouldThrow404()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(false);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            void action() => service.Delete(4);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Delete(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void Delete_DatabaseError_ShouldThrow400()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Delete(It.IsAny<int>())).Throws(new KeyNotFoundException());
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(true);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            void action() => service.Delete(1);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Edit_Success_ShouldEditTruck()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Edit(It.IsAny<Truck>()));
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(true);
            var service = new TruckService(logger, mockRepos.Object);
            var model = new TruckViewModel
            {
                Model = "FM",
                ProductionYear = 2019
            };

            // Act
            service.Edit(2, model);

            // Assert
            mockRepos.Verify(m => m.Edit(It.IsAny<Truck>()), Times.Once());
        }

        [Fact]
        public void Edit_DoesNotExist_ShouldThrow404()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(false);
            var service = new TruckService(logger, mockRepos.Object);
            var model = new TruckViewModel();

            // Act
            void action() => service.Edit(4, model);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Edit(It.IsAny<Truck>()), Times.Never());
        }

        [Fact]
        public void Edit_DatabaseError_ShouldThrow400()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Edit(It.IsAny<Truck>())).Throws(new Exception());
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(true);
            var service = new TruckService(logger, mockRepos.Object);
            var model = new TruckViewModel();

            // Act
            void action() => service.Edit(1, model);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Edit(It.IsAny<Truck>()), Times.Once());
        }

        [Fact]
        public void Get_Success_ReturnsTruck()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            var entity = new TruckCreator(2, "FH", 2020);
            mockRepos.Setup(m => m.Get(It.IsAny<int>())).Returns(entity);
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(true);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            var truck = service.Get(2);

            // Assert
            Assert.Equal(2, truck.Id);
            Assert.Equal("FH", truck.Model);
            Assert.Equal(2020, truck.ProductionYear);
            mockRepos.Verify(m => m.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Get_DoesNotExist_ShouldThrow404()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(false);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            void action() => service.Get(4);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Get(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void Get_DatabaseError_ShouldThrow400()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();
            mockRepos.Setup(m => m.Get(It.IsAny<int>())).Throws(new Exception());
            mockRepos.Setup(m => m.Exists(It.IsAny<int>())).Returns(true);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            void action() => service.Get(1);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(action);
            Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            mockRepos.Verify(m => m.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void GetAll_Success_ReturnsAllTrucks()
        {
            // Arrange
            var logger = GenerateFakeLogger();
            var mockRepos = GenerateFakeRepos();

            var entities = new List<Truck>
            {
                new TruckCreator(1, "FH", 2021, 2020),
                new TruckCreator(2, "FH", 2020, 2020),
                new TruckCreator(3, "FM", 2021, 2020)
            };

            mockRepos.Setup(m => m.GetAll()).Returns(entities);
            var service = new TruckService(logger, mockRepos.Object);

            // Act
            var trucks = service.GetAll();

            // Assert
            Assert.Collection(trucks, e =>
            {
                Assert.Equal(1, e.Id);
                Assert.Equal("FH", e.Model);
                Assert.Equal(2021, e.ProductionYear);
            }, e =>
            {
                Assert.Equal(2, e.Id);
                Assert.Equal("FH", e.Model);
                Assert.Equal(2020, e.ProductionYear);
            }, e =>
            {
                Assert.Equal(3, e.Id);
                Assert.Equal("FM", e.Model);
                Assert.Equal(2021, e.ProductionYear);
            });
            mockRepos.Verify(m => m.GetAll(), Times.Once());
        }


        private static ILogger<TruckService> GenerateFakeLogger()
        {
            return new NullLogger<TruckService>();
        }

        private static Mock<IRepository<Truck>> GenerateFakeRepos()
        {
            return new Mock<IRepository<Truck>>();
        }
    }
}
