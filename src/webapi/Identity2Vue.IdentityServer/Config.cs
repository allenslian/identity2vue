// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace Identity2Vue.IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// Id token and access token lifetime
        /// </summary>
        private const int TokenLifetimeInMins = 1;

        /// <summary>
        /// Refresh token lifetime
        /// </summary>
        private const int RefreshTokenLifetimeInMins = 6;

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
              {
                UserClaims = new [] {"name"}
              }
            };

        public static IEnumerable<Client> Clients(IConfiguration configuration)
        {
            var baseUris = configuration.GetSection("ClientBaseUris")?.Get<string[]>();
            if (baseUris == null || baseUris.Length == 0)
            {
                throw new ArgumentException("Missing ClientBaseUris section in configuration file!");
            }

            return new Client[]
            {
              new Client
              {
                ClientId = "webclient.vuejs",
                ClientName = "webclient.vuejs",
                RequireConsent = false,
                RequireClientSecret = false,

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = TokenLifetimeInMins * 60,
                UpdateAccessTokenClaimsOnRefresh = true,
                AllowAccessTokensViaBrowser = true,
                IdentityTokenLifetime = TokenLifetimeInMins * 60,
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                RefreshTokenUsage = TokenUsage.ReUse,
                AbsoluteRefreshTokenLifetime = RefreshTokenLifetimeInMins * 60,
                SlidingRefreshTokenLifetime = RefreshTokenLifetimeInMins * (60 / 2),
                // AlwaysIncludeUserClaimsInIdToken = true,

                FrontChannelLogoutSessionRequired = true,
                FrontChannelLogoutUri = baseUris[0],

                RedirectUris = new List<string>
                {
                    baseUris[0] + "/#/sign-in-callback",
                    baseUris[0] + "/silent-callback.html",
                    baseUris[1] + "/#/sign-in-callback",
                    baseUris[1] + "/silent-callback.html"
                },
                PostLogoutRedirectUris = new List<string>(baseUris),
                AllowedCorsOrigins = new List<string>(baseUris),
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
}