
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity2Vue.IdentityServer.Services
{
    public interface IUserStore
    {
        /// <summary>
        /// FindBySubjectIdAsync finds active user by subject id
        /// </summary>
        /// <param name="subjectId">subject id may be user id</param>
        /// <returns>IUserIdentity instance</returns>
        Task<IUserIdentity> FindBySubjectIdAsync(string subjectId);

        /// <summary>
        /// FindByCredentialsAsync will return the user identity.
        /// </summary>
        /// <param name="username">your username</param>
        /// <param name="password">your password</param>
        /// <returns>it's null, or not null</returns>
        Task<IUserIdentity> FindByCredentialsAsync(string username, string password);
    }

    public interface IUserIdentity
    {
        string Id { get; }

        string Username { get; }

        ICollection<Claim> Claims { get; }
    }
}