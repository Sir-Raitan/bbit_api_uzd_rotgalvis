﻿using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace IdServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("https://localhost:7299/api", "API")
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource{ 
                    Name = "api",
                    DisplayName = "API",
                    Scopes = new List<string> { @"https://localhost:7299/api" }
                }
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
                new Client
                {
                    ClientId = "spa",
                    ClientName = "Single page app",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "https://localhost:7299/api"
                    },
                    AllowedCorsOrigins = { "https://localhost:4200" },

                    RedirectUris = { "https://localhost:4200/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4200/authentication/logout-callback" }
                }
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    }
}