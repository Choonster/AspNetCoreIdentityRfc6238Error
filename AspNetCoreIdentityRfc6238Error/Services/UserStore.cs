using AspNetCoreIdentityRfc6238Error.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreIdentityRfc6238Error.Services
{
    public class UserStore : UserStoreBase<User, int, IdentityUserClaim<int>, IdentityUserLogin<int>, IdentityUserToken<int>>
    {
        private static readonly ConcurrentDictionary<User, bool> users = new ConcurrentDictionary<User, bool>
        {
            [new User
            {
                Id = 1,
                Email = "john@example.com",
                NormalizedEmail = "JOHN@EXAMPLE.COM",
                TwoFactorEnabled = true,
            }] = true,
        };

        private static readonly ConcurrentDictionary<IdentityUserToken<int>, bool> tokens = new ConcurrentDictionary<IdentityUserToken<int>, bool>();

        public UserStore(IdentityErrorDescriber errorDescriber = null) : base(errorDescriber ?? new IdentityErrorDescriber())
        {
        }

        public override IQueryable<User> Users => users.Keys.AsQueryable();

        public override Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        protected override Task AddUserTokenAsync(IdentityUserToken<int> token)
        {
            tokens.TryAdd(token, true);

            return Task.CompletedTask;
        }

        protected override Task<IdentityUserToken<int>> FindTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                tokens
                    .Keys
                    .Where(token => token.UserId == user.Id)
                    .Where(token => token.LoginProvider == loginProvider)
                    .Where(token => token.Name == name)
                    .FirstOrDefault()
            );
        }

        protected override Task<User> FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                users.Keys.FirstOrDefault(user => user.Id == userId)
            );
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken<int> token)
        {
            tokens.TryRemove(token, out _);

            return Task.CompletedTask;
        }

        public override Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<int>> FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<int>> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}