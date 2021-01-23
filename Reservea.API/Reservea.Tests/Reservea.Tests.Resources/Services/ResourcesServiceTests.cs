using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Helpers;
using Reservea.Microservices.Resources.Interfaces.Services;
using Reservea.Microservices.Resources.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Reservea.Tests.Resources.Services
{
    public class ResourcesServiceTests
    {
        [Fact]
        public async Task ValidateAsync_ResourceWithNoAvaiabilites_False()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                 new ResourceAvailability{
                    ResourceId = 2,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 12:00:00"),
                    End = DateTime.Parse("2020-01-01 14:00:00"),
                    Interval = null
                }
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:00"),
                    End = DateTime.Parse("2020-01-01 14:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationWithinSingleAviability_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 4:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:00"),
                    End = DateTime.Parse("2020-01-01 15:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationOutsideSingleAviabilityRightEdge_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 4:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:01"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationOutsideSingleAviabilityLeftEdge_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 4:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 3:59:59"),
                    End = DateTime.Parse("2020-01-01 5:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task ValidateAsync_ReservationWithinSingleAviabilityRightEdge_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 4:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationWithinSingleAviabilityLeftEdge_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 4:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 4:00:00"),
                    End = DateTime.Parse("2020-01-01 5:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.True(result);
        }



        [Fact]
        public async Task ValidateAsync_ReservationWithinTwoAvaiabilites_False()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 14:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 12:00:00"),
                    End = DateTime.Parse("2020-01-01 14:00:00"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:00"),
                    End = DateTime.Parse("2020-01-01 15:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationWithinTwoAvaiabilitesWithGap_False()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 14:00:00"),
                    End = DateTime.Parse("2020-01-01 16:00:00"),
                    Interval = null
                },
                new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = false,
                    Start = DateTime.Parse("2020-01-01 12:00:00"),
                    End = DateTime.Parse("2020-01-01 13:59:59"),
                    Interval = null
                },
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:00"),
                    End = DateTime.Parse("2020-01-01 15:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationWithinReccuringAvaiability_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                  new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = true,
                    Start = DateTime.Parse("2020-01-01 00:00:00"),
                    End = DateTime.Parse("2020-01-01 01:00:00"),
                    Interval = new TimeSpan(1,0,0)
                }
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 4:00:30"),
                    End = DateTime.Parse("2020-01-01 16:00:30"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationWithinReccuringAvaiabilityWithGap_False()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                  new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = true,
                    Start = DateTime.Parse("2020-01-01 00:00:00"),
                    End = DateTime.Parse("2020-01-01 00:59:59"),
                    Interval = new TimeSpan(1,0,0)
                }
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 4:00:30"),
                    End = DateTime.Parse("2020-01-01 16:00:30"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateAsync_ReservationWithinReccuringAndNormalAvaiability_True()
        {
            // Arange
            var resourceAvailabilities = new List<ResourceAvailability>
            {
                  new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = true,
                    Start = DateTime.Parse("2020-01-01 12:00:00"),
                    End = DateTime.Parse("2020-01-01 14:00:00"),
                    Interval = new TimeSpan(1,0,0)
                },
                   new ResourceAvailability{
                    ResourceId = 1,
                    IsReccuring = true,
                    Start = DateTime.Parse("2019-01-01 14:00:00"),
                    End = DateTime.Parse("2019-01-01 16:00:00"),
                    Interval = new TimeSpan(24,0,0)
                }
            };
            var service = GetResourcesServiceForTests(resourceAvailabilities);
            var reservations = new List<ReservationValidationRequest>
            {
                new ReservationValidationRequest
                {
                    Start = DateTime.Parse("2020-01-01 13:00:0"),
                    End = DateTime.Parse("2020-01-01 14:00:00"),
                    ResourceId = 1
                },
            };

            // Act
            var result = await service.ValidateAsync(reservations, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        #region Private helpers
        private IResourcesService GetResourcesServiceForTests(IEnumerable<ResourceAvailability> returnList)
        {
            var mapper = GetMapper();
            var resourcesUnitOfWork = GetMockedResourcesUnitOfWork(returnList);

            return new ResourcesService(resourcesUnitOfWork, mapper);
        }

        private IResourcesUnitOfWork GetMockedResourcesUnitOfWork(IEnumerable<ResourceAvailability> returnList)
        {
            var unitOfWorkMock = new Mock<IResourcesUnitOfWork>();

            unitOfWorkMock.Setup(x => x.ResourceAvailabilitiesRepository
            .GetAsync(
                It.IsAny<Expression<Func<ResourceAvailability, bool>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<Func<IQueryable<ResourceAvailability>, IIncludableQueryable<ResourceAvailability, object>>>()
            )).ReturnsAsync(returnList);

            return unitOfWorkMock.Object;
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });

            return new Mapper(config);
        }
        #endregion
    }
}
