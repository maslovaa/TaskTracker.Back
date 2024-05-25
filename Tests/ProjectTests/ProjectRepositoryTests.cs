namespace Tests.ProjectTests
{
    public class ProjectRepositoryTests
    {
        private Mock<IRepository<ProjectEntity, Guid>> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<ProjectEntity, Guid>>();
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEntities()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var entities = new List<ProjectEntity>
        {
            new() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now },
            new() { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
        }.AsQueryable();

            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>(), false))
                           .ReturnsAsync(entities);

            // Act
            var result = await _repositoryMock.Object.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.First().Id, Is.EqualTo(guid1));
            });
        }

        [Test]
        public void GetAllShouldReturnEntities()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var entities = new List<ProjectEntity>
        {
            new() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now },
            new() { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
        }.AsQueryable();

            _repositoryMock.Setup(repo => repo.GetAll(false))
                           .Returns(entities);

            // Act
            var result = _repositoryMock.Object.GetAll();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.First().Id, Is.EqualTo(guid1));
            });
        }

        [Test]
        public async Task AddAsyncShouldReturnNewProjectEntityId()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var ProjectEntity = new ProjectEntity() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now };
            _repositoryMock.Setup(repo => repo.AddAsync(ProjectEntity)).ReturnsAsync(ProjectEntity.Id);

            // Act
            var result = await _repositoryMock.Object.AddAsync(ProjectEntity);

            // Assert
            Assert.That(result, Is.EqualTo(guid1));
        }

        [Test]
        public void AddShouldReturnNewProjectEntityId()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var ProjectEntity = new ProjectEntity() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now };
            _repositoryMock.Setup(repo => repo.Add(ProjectEntity)).Returns(ProjectEntity.Id);

            // Act
            var result = _repositoryMock.Object.Add(ProjectEntity);

            // Assert
            Assert.That(result, Is.EqualTo(guid1));
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnCorrectProjectEntity()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var ProjectEntity = new ProjectEntity() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(guid1, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(ProjectEntity);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(guid1, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(guid1));
        }

        [Test]
        public void GetByIdShouldReturnCorrectProjectEntity()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var ProjectEntity = new ProjectEntity() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now };

            _repositoryMock.Setup(repo => repo.GetById(guid1))
                           .Returns(ProjectEntity);

            // Act
            var result = _repositoryMock.Object.GetById(guid1);

            // Assert
            Assert.That(result.Id, Is.EqualTo(guid1));
        }

        [Test]
        public async Task UpdateShouldReturnTrueWhenIsSuccessful()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var ProjectEntity = new ProjectEntity() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now };

            _repositoryMock.Setup(repo => repo.Update(ProjectEntity)).ReturnsAsync(true);

            // Act
            var result = await _repositoryMock.Object.Update(ProjectEntity);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetByPredicateShouldReturnFalseIfReturnsHaveWrongRecords()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var purposeDate = DateTime.Now.AddHours(-1);
            Expression<Func<ProjectEntity, bool>> predicate = e => e.StartDate > purposeDate;
            var entities = new List<ProjectEntity>
        {
            new() { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now.AddHours(-2) },
            new() { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
        }.AsQueryable();

            _repositoryMock.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<ProjectEntity, bool>>>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Expression<Func<ProjectEntity, bool>> pred, CancellationToken ct) =>
                        entities.Where(pred).ToList());

            // Act
            var result = await _repositoryMock.Object.GetByPredicate(predicate, CancellationToken.None);

            // Assert
            Assert.That(result.Any(x => x.StartDate <= purposeDate), Is.False);
        }

        [Test]
        public async Task DeleteShouldReturnTrueWhenIsSuccessful()
        {
            // Arrange
            var ProjectEntityId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.Delete(ProjectEntityId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _repositoryMock.Object.Delete(ProjectEntityId, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}