using IdServer.Models;

namespace IdServer
{
    public static class UserConfig
    {
        public static ApplicationUser ManagerUser =>
            new ApplicationUser
            {
                FirstName = "Janis",
                LastName = "Tests",
                UserName = "janis.tests",
                Email = "Janis.tests@fake.com"
            };
        public static ApplicationUser ResidentUser =>
           new ApplicationUser
           {
               FirstName = "Maris",
               LastName = "Rutks",
               UserName = "mar.rutks",
               Email = "rutks@fake.com"
           };
    }
}
