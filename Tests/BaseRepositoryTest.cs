using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Infrastructure;
using TruckRegistration.Models;
using Xunit;

namespace Tests
{
    public class BaseRepositoryTest
    {
        [Fact]
        public void Add_WithValidEntity_ShouldInsertToDb()
        {
            // Arrange
            using var mockContext = GenerateFakeContext();
            Seed(mockContext);
            var repos = new BaseRepository<Truck>(mockContext);
            var truck = new Truck { Model = "FM", ModelYear = 2022, ProductionYear = 2021 };

            // Act
            repos.Add(truck);

            // Assert
            Assert.Collection(mockContext.Set<Truck>(), e =>
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
            }, e =>
            {
                Assert.Equal(4, e.Id);
                Assert.Equal("FM", e.Model);
                Assert.Equal(2021, e.ProductionYear);
            });
        }

        [Fact]
        public void Delete_WithExistingId_ShouldRemoveFromDb()
        {
            // Arrange
            using var mockContext = GenerateFakeContext();
            Seed(mockContext);
            var repos = new BaseRepository<Truck>(mockContext);

            // Act
            repos.Delete(2);

            // Assert
            Assert.Collection(mockContext.Set<Truck>(), e =>
            {
                Assert.Equal(1, e.Id);
                Assert.Equal("FH", e.Model);
            }, e =>
            {
                Assert.Equal(3, e.Id);
                Assert.Equal("FM", e.Model);
            });
        }

        [Fact]
        public void Delete_WhenIdNotFound_ShouldThrow()
        {
            // Arrange
            using var stubContext = GenerateFakeContext();
            Seed(stubContext);
            var repos = new BaseRepository<Truck>(stubContext);

            // Act
            void action() => repos.Delete(4);

            // Assert
            Assert.Throws<KeyNotFoundException>(action);
        }

        [Fact]
        public void Edit_WithValidEntity_ShouldUpdateRecord()
        {
            // Arrange
            using var mockContext = GenerateFakeContext();
            Seed(mockContext);
            var repos = new BaseRepository<Truck>(mockContext);
            var truck = mockContext.Find<Truck>(2);
            truck.Model = "FM";
            truck.ProductionYear = 2019;

            // Act
            repos.Edit(truck);

            // Assert
            Assert.Collection(mockContext.Set<Truck>(), e =>
            {
                Assert.Equal(1, e.Id);
                Assert.Equal("FH", e.Model);
                Assert.Equal(2021, e.ProductionYear);
            }, e =>
            {
                Assert.Equal(2, e.Id);
                Assert.Equal("FM", e.Model);
                Assert.Equal(2019, e.ProductionYear);
            }, e =>
            {
                Assert.Equal(3, e.Id);
                Assert.Equal("FM", e.Model);
                Assert.Equal(2021, e.ProductionYear);
            });
        }

        [Fact]
        public void Edit_WhenIdNotFound_ShouldThrow()
        {
            // Arrange
            using var stubContext = GenerateFakeContext();
            Seed(stubContext);
            var repos = new BaseRepository<Truck>(stubContext);
            var truck = new Truck { Model = "FM", ModelYear = 2022, ProductionYear = 2021 };

            // Act
            void action() => repos.Edit(truck);

            // Assert
            Assert.Throws<KeyNotFoundException>(action);
        }

        [Fact]
        public void Exists_MatchesId_ReturnsTrue()
        {
            // Arrange
            using var stubContext = GenerateFakeContext();
            Seed(stubContext);
            var repos = new BaseRepository<Truck>(stubContext);

            // Act
            var exists = repos.Exists(2);

            // Assert
            Assert.True(exists, "Item not found, but should exist.");
        }

        [Fact]
        public void Exists_DoesNotMatchId_ReturnsFalse()
        {
            // Arrange
            using var stubContext = GenerateFakeContext();
            Seed(stubContext);
            var repos = new BaseRepository<Truck>(stubContext);

            // Act
            var exists = repos.Exists(4);

            // Assert
            Assert.False(exists, "Item found, but should not be.");
        }

        [Fact]
        public void Get_WithExistingId_ReturnsEntity()
        {
            // Arrange
            using var stubContext = GenerateFakeContext();
            Seed(stubContext);
            var repos = new BaseRepository<Truck>(stubContext);

            // Act
            var entity = repos.Get(2);

            // Assert
            Assert.Equal(2, entity.Id);
            Assert.Equal("FH", entity.Model);
            Assert.Equal(2020, entity.ProductionYear);
        }

        [Fact]
        public void Get_WhenIdNotFound_ReturnsNull()
        {
            // Arrange
            using var stubContext = GenerateFakeContext();
            Seed(stubContext);
            var repos = new BaseRepository<Truck>(stubContext);

            // Act
            var entity = repos.Get(4);

            // Assert
            Assert.Null(entity);
        }

        [Fact]
        public void GetAll_Success_ReturnsAllEntities()
        {
            // Arrange
            using var mockContext = GenerateFakeContext();
            Seed(mockContext);
            var repos = new BaseRepository<Truck>(mockContext);

            // Act
            var entities = repos.GetAll();

            // Assert
            Assert.Collection(mockContext.Set<Truck>(), e =>
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
        }

        private static AppDbContext GenerateFakeContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ReposTestDb")
                .Options;

            return new AppDbContext(options);
        }

        private static void Seed(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var truck1 = new Truck { Model = "FH", ModelYear = 2020, ProductionYear = 2021 };
            var truck2 = new Truck { Model = "FH", ModelYear = 2020, ProductionYear = 2020 };
            var truck3 = new Truck { Model = "FM", ModelYear = 2020, ProductionYear = 2021 };

            context.AddRange(truck1, truck2, truck3);
            context.SaveChanges();
        }
    }
}
