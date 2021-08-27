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
          new ApiScope("platform.api", "platform api"),
          new ApiScope("offline_access", "offline access")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
          new Client
          {
            ClientId = "webclient.vuejs",
            ClientName = "webclient.vuejs",
            AllowOfflineAccess = true,
            AccessTokenType = AccessTokenType.Jwt,
            RequireConsent = false,
            AccessTokenLifetime = 60,
            UpdateAccessTokenClaimsOnRefresh = true,
            IdentityTokenLifetime = 60,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            RefreshTokenUsage = TokenUsage.ReUse,
            AbsoluteRefreshTokenLifetime = 360,
            SlidingRefreshTokenLifetime = 180,

            RequireClientSecret = false,
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,

            AllowAccessTokensViaBrowser = true,
            FrontChannelLogoutSessionRequired = true,
            FrontChannelLogoutUri = "http://localhost:8080/#/sign-out-callback",
            
            RedirectUris = new List<string>
            {
                "http://localhost:8080/#/sign-in-callback",
                "http://localhost:8080/silent-callback.html"
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
                "platform.api",
                "offline_access"
            }
          }
        };
  }
}