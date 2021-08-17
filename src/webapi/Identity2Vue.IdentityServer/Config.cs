// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity2Vue.IdentityServer
{
  public static class Config
  {
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
          new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
          new ApiScope("platform.api", "platform api")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
          new Client
          {
            ClientId = "vuejs.client",
            ClientName = "vuejs.client",
            // AccessTokenType = AccessTokenType.Jwt,
            // RequireConsent = false,
            AccessTokenLifetime = 300,// 330 seconds, default 60 minutes
            IdentityTokenLifetime = 300,

            RequireClientSecret = false,
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,

            AllowAccessTokensViaBrowser = true,
            RedirectUris = new List<string>
            {
                "https://localhost:44357",
                "https://localhost:44357/callback.html",
                "https://localhost:44357/silent-renew.html"
            },
            PostLogoutRedirectUris = new List<string>
            {
                "https://localhost:44357/",
                "https://localhost:44357"
            },
            AllowedCorsOrigins = new List<string>
            {
                "https://localhost:44357"
            },
            AllowedScopes = new List<string>
            {
                "openid",
                "profile",
                "platform.api"
            }
          }
        };
  }
}