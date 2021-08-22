
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
        /// ValidateCredentialAsync verify whether username and password is correct.
        /// </summary>
        /// <param name="username">your username</param>
        /// <param name="password">your password</param>
        /// <returns>true is ok, false is failed</returns>
        Task<bool> ValidateCredentialsAsync(string username, string password);

        /// <summary>
        /// FindByUsernameAsync finds active user by username
        /// </summary>
        /// <param name="username">your username</param>
        /// <returns>IUserIdentity instance</returns>
        Task<IUserIdentity> FindByUsernameAsync(string username);
    }

    public interface IUserIdentity
    {
        string Id { get; }

        string Username { get; }

        ICollection<Claim> Claims { get; }
    }
}