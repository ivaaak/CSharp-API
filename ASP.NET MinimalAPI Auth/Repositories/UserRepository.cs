using MinimalJwt.Models;

namespace MinimalJwt.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new() { Username = "admin", EmailAddress = "admin@email.com", Password = "password", GivenName = "Dani", Surname = "Danielov", Role = "Administrator" },
            new() { Username = "standard", EmailAddress = "standard@email.com", Password = "password", GivenName = "Gosho", Surname = "Goshov", Role = "Standard" },
        };
    }
}
