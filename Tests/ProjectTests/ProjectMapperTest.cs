namespace Tests.ProjectTests
{
    public class ProjectMapperTest
    {
        public class MappingTests
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
            public void ShouldMapProjectEntityToProjectDto()
            {
                // Arrange
                var entity = new ProjectEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Project1",
                    Description = "Тестовый проект 1",
                    Status = "New",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1)
                };

                // Act
                var dto = _mapper.Map<ProjectDto>(entity);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(dto.Id, Is.EqualTo(entity.Id));
                    Assert.That(dto.Name, Is.EqualTo(entity.Name));
                    Assert.That(dto.Description, Is.EqualTo(entity.Description));
                    Assert.That(dto.Status, Is.EqualTo(entity.Status));
                    Assert.That(dto.StartDate, Is.EqualTo(entity.StartDate));
                    Assert.That(dto.EndDate, Is.EqualTo(entity.EndDate));
                });
            }

            [Test]
            public void ShouldMapProjectDtoToProjectEntity()
            {
                // Arrange
                var dto = new ProjectDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Project1",
                    Description = "Тестовый проект 1",
                    Status = "New",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1)
                };

                // Act
                var entity = _mapper.Map<ProjectEntity>(dto);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(entity.Id, Is.EqualTo(dto.Id));
                    Assert.That(entity.Name, Is.EqualTo(dto.Name));
                    Assert.That(entity.Description, Is.EqualTo(dto.Description));
                    Assert.That(entity.Status, Is.EqualTo(dto.Status));
                    Assert.That(entity.StartDate, Is.EqualTo(dto.StartDate));
                    Assert.That(entity.EndDate, Is.EqualTo(dto.EndDate));
                });
            }
        }
    }
}
