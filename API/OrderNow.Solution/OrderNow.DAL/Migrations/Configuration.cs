using System.Data.Entity.Migrations; 
using OrderNow.Common;
using OrderNow.DAL.Entities;
using OrderNow.DAL.Entities.Model;

namespace OrderNow.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<OrderNowContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OrderNowContext context)
        {
            //context.Users.Add(new Admin
            //{
            //    IsDeleted = false,
            //    Password = "ADX4VeuwJ0BAoXSOXntYdJQJoRd61V+N/TNePnJfu38=",
            //    UserName = "admin",
            //    Role = Enums.RoleType.Admin
            //});
            ////This method will be called after migrating to the latest version.

            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 1
            //});
            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 2
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 3
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 4
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 5
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 6
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 7
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 8
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 9
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 10
            //});
            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 11
            //});
            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 12
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 13
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 14
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 15
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 16
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 17
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 18
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 19
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 20
            //});


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
