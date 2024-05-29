using DataAccess;
using DataAccess.Repositories;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class DeskRepositoryTests
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
        public void Add_New_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity();
                Guid guid = repository.Add(entity);

                Assert.That(entity.Id, Is.EqualTo(guid));
            }
        }

        [Test]
        public async Task AddAsync_New_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity();
                Guid guid = await repository.AddAsync(entity);

                Assert.That(entity.Id, Is.EqualTo(guid));
            }
        }

        [Test]
        public void GetById_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity();
                context.DeskEntities.Add(entity);
                context.SaveChanges();

                DeskEntity desk = repository.GetById(entity.Id);

                Assert.That(desk.Id, Is.EqualTo(entity.Id));
            }
        }

        [Test]
        public async Task GetByIdAsync_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity();
                await context.DeskEntities.AddAsync(entity);
                await context.SaveChangesAsync();

                DeskEntity desk = await repository.GetByIdAsync(entity.Id, CancellationToken.None);

                Assert.That(desk.Id, Is.EqualTo(entity.Id));
            }
        }

        [Test]
        public void GetAll_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity1 = new DeskEntity
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                };
                DeskEntity entity2 = new DeskEntity
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                };

                context.DeskEntities.Add(entity1);
                context.DeskEntities.Add(entity2);

                context.SaveChanges();

                var result = repository.GetAll().ToList();

                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(entity1.Id));
                });
            }
        }

        [Test]
        public async Task GetByPredicateAsync_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity1 = new DeskEntity();
                DeskEntity entity2 = new DeskEntity();
                Expression<Func<DeskEntity, bool>> predicate = e => e.Id != Guid.Empty;

                await context.DeskEntities.AddAsync(entity1);
                await context.DeskEntities.AddAsync(entity2);

                await context.SaveChangesAsync();

                var result = await repository.GetByPredicateAsync(predicate, CancellationToken.None);

                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(entity1.Id));
                });
            }
        }

        [Test]
        public void GetByPredicate_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity1 = new DeskEntity();
                DeskEntity entity2 = new DeskEntity();
                Expression<Func<DeskEntity, bool>> predicate = e => e.Id != Guid.Empty;

                context.DeskEntities.Add(entity1);
                context.DeskEntities.Add(entity2);

                context.SaveChanges();

                var result = repository.GetByPredicate(predicate);

                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(entity1.Id));
                });
            }
        }

        [Test]
        public async Task UpdateAsync_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity
                { 
                    Id = Guid.NewGuid(),
                    Name = "Test 1"
                };

                await context.DeskEntities.AddAsync(entity);

                await context.SaveChangesAsync();

                DeskEntity? update = await context.DeskEntities.FindAsync(entity.Id);

                update.Name = "Test 2";

                bool isupdated = await repository.UpdateAsync(update, CancellationToken.None);

                DeskEntity? result = await context.DeskEntities.FindAsync(entity.Id);

                Assert.Multiple(() =>
                {
                    Assert.That(result?.Name, Is.EqualTo(update.Name));
                    Assert.That(isupdated, Is.EqualTo(true));
                });
            }
        }

        [Test]
        public void Update_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Test 1"
                };

                context.DeskEntities.Add(entity);

                context.SaveChanges();

                DeskEntity? update = context.DeskEntities.Find(entity.Id);

                update.Name = "Test 2";

                bool isupdated = repository.Update(update);

                DeskEntity? result = context.DeskEntities.Find(entity.Id);

                Assert.Multiple(() =>
                {
                    Assert.That(result?.Name, Is.EqualTo(update.Name));
                    Assert.That(isupdated, Is.EqualTo(true));
                });
            }
        }

        [Test]
        public async Task DeleteAsync_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Test 1"
                };

                await context.DeskEntities.AddAsync(entity);

                await context.SaveChangesAsync();

                DeskEntity? delete = await context.DeskEntities.FindAsync(entity.Id);

                bool isdeleted = await repository.DeleteAsync(delete.Id, CancellationToken.None);

                DeskEntity? result = await context.DeskEntities.FindAsync(entity.Id);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsActive, Is.EqualTo(false));
                    Assert.That(isdeleted, Is.EqualTo(true));
                });
            }
        }

        [Test]
        public void Delete_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity = new DeskEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Test 1"
                };

                context.DeskEntities.Add(entity);

                context.SaveChanges();

                DeskEntity? delete = context.DeskEntities.Find(entity.Id);

                bool isdeleted = repository.Delete(delete.Id);

                DeskEntity? result = context.DeskEntities.Find(entity.Id);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsActive, Is.EqualTo(false));
                    Assert.That(isdeleted, Is.EqualTo(true));
                });
            }
        }
    }
}
