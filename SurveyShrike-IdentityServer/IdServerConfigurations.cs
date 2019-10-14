﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace SurveyShrike_IdentityServer
{
    public class IdServerConfigurations
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api", "SurveyShrike- Apis to manage surveys")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedCorsOrigins =     { "http://localhost:8080" },
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "api" }
                },
                new Client
                {
                    ClientId = "spa",
                    ClientName = "Single Page Javascript App",
                    AllowedGrantTypes = GrantTypes.Code,
                    // Specifies whether this client can request refresh tokens
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,
                    
                    // no consent page
                    RequireConsent = false,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:8080/callback.html" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:8080/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:8080" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api"
                    }
                }
            };
        }

        internal static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser { SubjectId = "1", Username = "ankit.kumar@example.com", Password = "@nkit!",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Ankit Kumar"),
                        new Claim(JwtClaimTypes.Email, "ankit.kumar@example.com")
                    }
                }
              
            };
        }
    }
}