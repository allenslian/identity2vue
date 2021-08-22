
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace Identity2Vue.IdentityServer.Services
{
    public class DefaultProfileService : IProfileService
    {
        protected ILogger Logger;

        protected IUserStore Store;

        public DefaultProfileService(
            IUserStore store,
            ILogger<DefaultProfileService> logger)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            Logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            if (context.RequestedClaimTypes.Any())
            {
                var user = await Store.FindBySubjectIdAsync(context.Subject.GetSubjectId());
                if (user != null)
                {
                    context.AddRequestedClaims(user.Claims);
                }
            }

            context.LogIssuedClaims(Logger);
        }

        public async virtual Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = await Store.FindBySubjectIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}