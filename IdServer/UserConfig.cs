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
               Email = "marutks@sivais.lv",
               ResidentId = "1688fdec-4ca3-4906-ab1d-c4c027b0f152"
           };
    }
}