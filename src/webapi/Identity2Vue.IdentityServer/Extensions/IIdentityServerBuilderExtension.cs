using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class IIdentityServerBuilderExtension
  {
    public static IIdentityServerBuilder AddCredential(this IIdentityServerBuilder builder, string privateKey)
    {
      var rsa = new RSACryptoServiceProvider(2048);
      rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
      builder.AddSigningCredential(new RsaSecurityKey(rsa), "RS256");
      return builder;
    }
  }
}