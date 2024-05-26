namespace Tests.ProjectTests
{
    public class ProjectRepositoryTests
    {
        private DbContextOptions<DataContext> _options;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new DataContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEntities()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.AddRange(
                    new ProjectEntity { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now },
                    new ProjectEntity { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
                );
                context.SaveChanges();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = await repository.GetAllAsync(CancellationToken.None);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(guid1));
                });
            }
        }

        [Test]
        public void GetAllShouldReturnEntities()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.AddRange(
                    new ProjectEntity { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now },
                    new ProjectEntity { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
                );
                context.SaveChanges();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = repository.GetAll();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(guid1));
                });
            }
        }


        [Test]
        public async Task AddAsyncShouldReturnNewProjectEntityId()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var projectEntity = new ProjectEntity()
            {
                Id = guid1,
                Description = "Тестовый проект 1",
                Name = "Project1",
                Status = "Новый",
                StartDate = DateTime.Now
            };

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = await repository.AddAsync(projectEntity);

                // Assert
                Assert.That(result, Is.EqualTo(guid1));

                // Дополнительно проверим, что сущность действительно добавлена в базу данных
                var addedEntity = await context.ProjectEntities.FindAsync(guid1);
                Assert.Multiple(() =>
                {
                    Assert.That(addedEntity, Is.Not.Null);
                    Assert.That(addedEntity?.Name, Is.EqualTo("Project1"));
                    Assert.That(addedEntity?.Description, Is.EqualTo("Тестовый проект 1"));
                });
            }
        }

        [Test]
        public void AddShouldReturnNewProjectEntityId()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var projectEntity = new ProjectEntity()
            {
                Id = guid1,
                Description = "Тестовый проект 1",
                Name = "Project1",
                Status = "Новый",
                StartDate = DateTime.Now
            };

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = repository.Add(projectEntity);

                // Assert
                Assert.That(result, Is.EqualTo(guid1));

                // Дополнительно проверим, что сущность действительно добавлена в базу данных
                var addedEntity = context.ProjectEntities.Find(guid1);
                Assert.Multiple(() =>
                {
                    Assert.That(addedEntity, Is.Not.Null);
                    Assert.That(addedEntity?.Name, Is.EqualTo("Project1"));
                    Assert.That(addedEntity?.Description, Is.EqualTo("Тестовый проект 1"));
                });
            }
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnCorrectProjectEntity()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.AddRange(
                    new ProjectEntity { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now },
                    new ProjectEntity { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
                );
                context.SaveChanges();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = await repository.GetByIdAsync(guid1, CancellationToken.None);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result?.Name, Is.EqualTo("Project1"));
                    Assert.That(result?.Description, Is.EqualTo("Тестовый проект 1"));
                });
            }
        }

        [Test]
        public void GetByIdShouldReturnCorrectProjectEntity()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.AddRange(
                    new ProjectEntity { Id = guid1, Description = "Тестовый проект 1", Name = "Project1", Status = "Новый", StartDate = DateTime.Now },
                    new ProjectEntity { Id = guid2, Description = "Тестовый проект 2", Name = "Project2", Status = "Новый", StartDate = DateTime.Now }
                );
                context.SaveChanges();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = repository.GetById(guid1);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result?.Name, Is.EqualTo("Project1"));
                    Assert.That(result?.Description, Is.EqualTo("Тестовый проект 1"));
                });
            }
        }

        [Test]
        public async Task UpdateAsyncShouldReturnTrueAndModifyEntity()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var projectEntity = new ProjectEntity()
            {
                Id = guid1,
                Description = "Тестовый проект 1",
                Name = "Project1",
                Status = "Новый",
                StartDate = DateTime.Now
            };

            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.Add(projectEntity);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Обновляем сущность
                projectEntity.Description = "Обновленный тестовый проект";
                projectEntity.Status = "В процессе";

                // Act
                var result = await repository.Update(projectEntity);

                // Assert
                Assert.That(result, Is.True);

                // Дополнительно проверим, что сущность действительно была обновлена в базе данных
                var updatedEntity = await context.ProjectEntities.FindAsync(guid1);
                Assert.Multiple(() =>
                {
                    Assert.That(updatedEntity, Is.Not.Null);
                    Assert.That(updatedEntity?.Description, Is.EqualTo("Обновленный тестовый проект"));
                    Assert.That(updatedEntity?.Status, Is.EqualTo("В процессе"));
                });
            }
        }


        [Test]
        public async Task GetByPredicateShouldReturnCorrectEntities()
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
            };

            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.AddRange(entities);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = await repository.GetByPredicate(predicate, CancellationToken.None);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Any(x => x.StartDate <= purposeDate), Is.False);
                    Assert.That(result.Count(), Is.EqualTo(1));
                    Assert.That(result.First().Id, Is.EqualTo(guid2));
                });
            }
        }

        [Test]
        public async Task DeleteShouldReturnTrueWhenIsSuccessful()
        {
            // Arrange
            var ProjectEntityId = Guid.NewGuid();

            // Создаем сущность для добавления в базу данных
            var entity = new ProjectEntity
            {
                Id = ProjectEntityId,
                Description = "Тестовый проект",
                Name = "Project",
                Status = "Новый",
                StartDate = DateTime.Now
            };

            using (var context = new DataContext(_options))
            {
                context.ProjectEntities.Add(entity);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(_options))
            {
                var repository = new ProjectsRepository(context);

                // Act
                var result = await repository.Delete(ProjectEntityId, CancellationToken.None);

                // Assert
                Assert.That(result, Is.True);

                // Проверяем, что сущность действительно удалена из базы данных
                var deletedEntity = await context.ProjectEntities.FindAsync(ProjectEntityId);
                Assert.That(deletedEntity, Is.Null);
            }
        }
    }
}