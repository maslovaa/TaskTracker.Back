using AutoMapper;
using Domain.Entities;
using Models.Dto;
using Services.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class DeskMapperTest
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Map_DeskEntity_To_DeskDto()
        {
            // Arrange
            var entity = new DeskEntity
            {
                Id = Guid.NewGuid(),
                Name = "Desk 1"
            };

            // Act
            var dto = _mapper.Map<DeskDto>(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(entity.Id));
                Assert.That(dto.Name, Is.EqualTo(entity.Name));
            });
        }

        [Test]
        public void Map_DeskDto_To_DeskEntity()
        {
            // Arrange
            var dto = new DeskDto
            {
                Id = Guid.NewGuid(),
                Name = "Desk 1"
            };

            // Act
            var entity = _mapper.Map<DeskEntity>(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entity.Id, Is.EqualTo(dto.Id));
                Assert.That(entity.Name, Is.EqualTo(dto.Name));
            });
        }
    }
}
