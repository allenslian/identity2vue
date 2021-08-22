

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity2Vue.IdentityServer.Services
{
    public class UserStore : IUserStore
    {
        private IList<UserIdentity> _users = new List<UserIdentity>{
            new UserIdentity{
                Id = "ac841b16-d214-49ad-909a-65cb31ddffa1",
                Username = "allen",
                Password = "321456",
                IsActive = true,
                Claims = new List<Claim>
                {
                    new Claim("name", "allen"),
                    new Claim("email", "allen.lian@outlook.com")
                }
            },
            new UserIdentity{
                Id = "ac841b16-d214-49ad-909a-65cb31ddffa2",
                Username = "bill",
                Password = "123456",
                IsActive = false,
                Claims = new List<Claim>
                {
                    new Claim("name", "bill"),
                    new Claim("email", "bill.gates@outlook.com")
                }
            }
        };

        public Task<IUserIdentity> FindBySubjectIdAsync(string subjectId)
        {
            var user = _users.FirstOrDefault(u => u.Id == subjectId && u.IsActive);
            return Task.FromResult<IUserIdentity>(user);
        }

        public Task<IUserIdentity> FindByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.IsActive);
            return Task.FromResult<IUserIdentity>(user);
        }

        public Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            // password + salt
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive);
            return Task.FromResult(user != null);
        }
    }

    internal class UserIdentity : IUserIdentity
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }
}