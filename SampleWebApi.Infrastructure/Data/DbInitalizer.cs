using Microsoft.EntityFrameworkCore;

namespace SampleWebApi.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Add seed data for User entity
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Dmitry",
                    LastName = "Post",
                    CreatedDate = DateTime.Now,
                    Email = "dmitrypost@email.com"
                }
            );

            // Additional seed data can be added here
        }
    }
}