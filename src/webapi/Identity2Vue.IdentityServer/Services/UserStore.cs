
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

using Flurl.Http;
using IdentityModel;

namespace Identity2Vue.IdentityServer.Services
{
    public class UserStore : IUserStore
    {
        private readonly Uri _platformBaseUri;

        private readonly int _expiration;

        private readonly IMemoryCache _cache;

        public UserStore(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var baseUri = config.GetValue<string>("Endpoints:PlatformBase", null)
                ?? throw new ArgumentException("缺少PlatformBase设置项!");
            _platformBaseUri = new Uri(baseUri, UriKind.Absolute);

            _expiration = config.GetValue("Tokens:LifetimeInHours", Config.TokenDefaultLifetimeInHours);
        }

        public async Task<IUserIdentity> FindBySubjectIdAsync(string subjectId)
        {
            if (_cache.TryGetValue($"users:{subjectId}", out UserIdentity identityCached))
            {
                return await Task.FromResult(identityCached);
            }
            return null;
        }

        public async Task<IUserIdentity> FindByCredentialsAsync(string username, string password)
        {
            var result = await new Uri(_platformBaseUri, "/dzpt/v1/login/LoginNew")
                .PostJsonAsync(new
                {
                    userName = username,
                    password
                })
                .ReceiveJson();
            if (result.data.code == 200)
            {
                var identity = new UserIdentity
                {
                    Id = result.data.data.guid,
                    Username = result.data.data.userName,
                    Claims = new List<Claim>{
                        new Claim(JwtClaimTypes.Name, result.data.data.userName)
                    }
                };
                _cache.Set($"users:{identity.Id}", identity, new MemoryCacheEntryOptions{
                    SlidingExpiration = TimeSpan.FromHours(_expiration)
                });
                return identity;
            }
            else
            {
                return null;
            }
        }
    }

    internal class UserIdentity : IUserIdentity
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }
}