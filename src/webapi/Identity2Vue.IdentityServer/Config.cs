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
          new IdentityResources.OpenId(),
          new IdentityResources.Profile()
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
            ClientId = "webclient.vuejs",
            ClientName = "webclient.vuejs",
            AccessTokenType = AccessTokenType.Jwt,
            RequireConsent = false,
            AccessTokenLifetime = 60,// 330 seconds, default 60 minutes
            UpdateAccessTokenClaimsOnRefresh = true,
            // IdentityTokenLifetime = 30,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            SlidingRefreshTokenLifetime = 60,

            RequireClientSecret = false,
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,

            // AllowAccessTokensViaBrowser = true,
            FrontChannelLogoutSessionRequired = true,
            FrontChannelLogoutUri = "http://localhost:8080/#/sign-out-callback",
            
            RedirectUris = new List<string>
            {
                "http://localhost:8080/#/sign-in-callback",
            },
            PostLogoutRedirectUris = new List<string>
            {
                "http://localhost:8080/#/sign-out-callback"
            },
            
            AllowedCorsOrigins = new List<string>
            {
                "http://localhost:8080"
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