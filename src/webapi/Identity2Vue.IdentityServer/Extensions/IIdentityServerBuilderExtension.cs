using System;
using System.Security.Cryptography;
using Identity2Vue.IdentityServer.Services;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IIdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddCredential(this IIdentityServerBuilder builder, string rsaPrivateKey)
        {
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(rsaPrivateKey), out _);
            builder.AddSigningCredential(new RsaSecurityKey(rsa), "RS256");
            return builder;
        }

        public static IIdentityServerBuilder AddUserStore<T>(this IIdentityServerBuilder builder) where T : class, IUserStore
        {
            builder.Services.AddScoped<IUserStore, T>();

            builder.AddProfileService<DefaultProfileService>()
              .AddResourceOwnerValidator<DefaultResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}