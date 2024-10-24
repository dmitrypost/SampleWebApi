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
                    Email = "dmitrypost@email.com",
                    PasswordHash = "SZ6O6PbFqal7qNepYj3chw==:8lnGbnnfnQyJdsCeBXZoO6beNP2uy2leFxz+rIU3f2w="
                }
            );

            // Additional seed data can be added here
        }
    }
}