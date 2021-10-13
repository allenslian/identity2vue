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
        public const int TokenDefaultLifetimeInHours = 6;

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

        public static IEnumerable<Client> Clients(IConfiguration config)
        {
            var tokenLifetimeInSeconds = (int)TimeSpan.FromHours(
              config.GetValue("Tokens:LifetimeInHours", TokenDefaultLifetimeInHours)).TotalSeconds;

            var clients = new List<Client>();
            var options = new List<ClientOption>();
            config.GetSection("Clients")?.Bind(options);
            if (options == null || options.Count == 0)
            {
                throw new ArgumentException("Missing Clients section in configuration file!");
            }

            foreach (var option in options)
            {
              clients.Add(new Client{
                ClientId = option.Id,
                ClientName = option.Name,
                RequireConsent = false,
                RequireClientSecret = false,

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = tokenLifetimeInSeconds,
                UpdateAccessTokenClaimsOnRefresh = true,
                AllowAccessTokensViaBrowser = true,
                IdentityTokenLifetime = tokenLifetimeInSeconds,
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                RefreshTokenUsage = TokenUsage.ReUse,
                AbsoluteRefreshTokenLifetime = tokenLifetimeInSeconds * 7,  // 7 times
                SlidingRefreshTokenLifetime = tokenLifetimeInSeconds - 300, //advanced 5 mins
                // AlwaysIncludeUserClaimsInIdToken = true,
                // FrontChannelLogoutSessionRequired = true,
                // FrontChannelLogoutUri = baseUris[0],

                RedirectUris = option.RedirectUris,
                PostLogoutRedirectUris = option.PostLogoutRedirectUris,
                AllowedCorsOrigins = option.AllowedCorsOrigins,
                AllowedScopes = option.AllowedScopes
              });
            }

            return clients;
        }
    }

    internal class ClientOption
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string[] PostLogoutRedirectUris { get; set; }

        public string[] RedirectUris { get; set; }

        public string[] AllowedCorsOrigins { get; set; }

        public string[] AllowedScopes { get; set; }
    }
}