using DataAccess;
using DataAccess.Repositories;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

                DeskEntity entity1 = new DeskEntity();
                DeskEntity entity2 = new DeskEntity();

                context.DeskEntities.Add(entity1);
                context.DeskEntities.Add(entity2);

                context.SaveChanges();

                var result = repository.GetAll();

                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(entity1.Id));
                });
            }
        }

        [Test]
        public async Task GetAllAsync_Desk_Test()
        {
            using (DataContext context = new DataContext(_options))
            {
                var repository = new DesksRepository(context);

                DeskEntity entity1 = new DeskEntity();
                DeskEntity entity2 = new DeskEntity();

                await context.DeskEntities.AddAsync(entity1);
                await context.DeskEntities.AddAsync(entity2);

                await context.SaveChangesAsync();

                var result = await repository.GetAllAsync(CancellationToken.None);

                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Id, Is.EqualTo(entity1.Id));
                });
            }
        }
    }
}
