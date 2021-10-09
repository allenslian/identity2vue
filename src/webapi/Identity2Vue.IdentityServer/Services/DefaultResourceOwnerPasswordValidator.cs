using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;

namespace Identity2Vue.IdentityServer.Services
{
    public class DefaultResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        protected IUserStore Store;

        protected ILogger Logger;

        public DefaultResourceOwnerPasswordValidator(
            IUserStore store,
            ILogger<DefaultResourceOwnerPasswordValidator> logger)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async virtual Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await Store.FindByCredentialsAsync(context.UserName, context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                   user.Id ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                   OidcConstants.AuthenticationMethods.Password, user.Claims);
            }
        }
    }
}