using Domain.Entities;

namespace DataAccess
{
    public static class DbSeeder
    {
        public static void Seed(DataContext context)
        {
            // Создадим тестовые роли
            var testRoles = new List<RoleEntity>
            {
                new() { Name = "Owner", Description = "Продукт овнер"},
                new() { Name = "Developer", Description = "Разработчик"}
            };

            // Создадим тестовых пользователей
            var testUsers = new List<UserEntity>
            {
                new() { UserName = "User1", Email = "User1@tasktracker.ru", Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", Role = testRoles[0]},
                new() { UserName = "User2", Email = "User2@tasktracker.ru", Name = "Сидр", Surname = "Сидоров", Patronymic = "Сидорович", Role = testRoles[1]}
            };

            // Создадим тестовый проект
            var testProjects = new List<ProjectEntity>
            {
                new() {
                    Name = "Project1",
                    Description = "Тестовый проект 1",
                    Owner = testUsers[0],
                    Users = testUsers,
                    StartDate = DateTime.UtcNow,
                    Status = "New"}
            };

            // Создадим тестовую доску
            var testDesks = new List<DeskEntity>
            {
                new() {
                    Name = "Desk1",
                    Description = "Доска задач",
                    Project = testProjects[0]
                }
            };
               
            // Создадим тестовые задачи
            var testTasks = new List<TaskEntity>
            {
                new() {
                    Head = "Задача 1",
                    Body = "Реализовать что то хорошее",
                    Desk = testDesks[0],
                    Performer =testUsers.FirstOrDefault(x => x.UserName == "User1")
                },
                new() { 
                    Head = "Задача 2", 
                    Body = "Реализовать что то плохое",
                    Desk = testDesks[0],
                    Performer = testUsers.FirstOrDefault(x => x.UserName == "User2")}
            };

            context.RolesEntities.AddRange(testRoles);
            context.Users.AddRange(testUsers);
            context.ProjectEntities.AddRange(testProjects);
            context.DeskEntities.AddRange(testDesks);
            context.TaskEntities.AddRange(testTasks);

            // Сохраним изменения в базе данных
            context.SaveChanges();
        }
    }
}
