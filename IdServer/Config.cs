using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

namespace IdServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("https://localhost:7299/api", "API"),
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("api","API"){
                    Scopes = new List<string> { @"https://localhost:7299/api" },
                },
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    //principa prieks swagger testesanas
                    ClientId = "client",

                    ClientName = "Console App",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "https://localhost:7299/api" },
                    AllowedCorsOrigins = { "https://localhost:7299" }
                },
                // interactive Web App
                //nemu no piemera - izmanto ja web app taisa no asp.net un ne ka spa
                new Client
                {
                    ClientId = "web",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
            
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:4200/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:4200/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "https://localhost:7299/api"
                    }
                },
                //Spa auth
                //lifetime uzlikts mazs, lai nevajadzetu ilgi gaidit
                new Client
                {
                    ClientId = "spa",
                    ClientName = "Apartment management app",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenLifetime = 300,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "https://localhost:7299/api",
                        "roles"
                    },
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = { "https://localhost:4200" },

                    RedirectUris = { "https://localhost:4200/" },
                    PostLogoutRedirectUris = { "https://localhost:4200/" }
                }
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource("roles","User Role", new List<string>() { "role", "resident_id" })
        };

    }
}
