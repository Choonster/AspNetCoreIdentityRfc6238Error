using AspNetCoreIdentityRfc6238Error.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetCoreIdentityRfc6238Error.Controllers
{
    [RoutePrefix("2fa")]
    public class TwoFactorController : ApiController
    {
        private readonly UserManager<User> _userManager;

        public TwoFactorController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Route("test")]
        [HttpGet]
        public async Task<IHttpActionResult> Test2faAsync()
        {
            var user = _userManager.Users.First();

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                var resetAuthenticatorKeyResult = await _userManager.ResetAuthenticatorKeyAsync(user);

                if (!resetAuthenticatorKeyResult.Succeeded)
                {
                    return Ok(resetAuthenticatorKeyResult);
                }
            }

            var verificationCode = "INVALID_2FA_CODE";

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                return Ok(IdentityResult.Failed(new IdentityError { Code = "invalid_code", Description = "2FA code was invalid" }));
            }

            var enableTwoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, true);

            return Ok(enableTwoFactorResult);
        }
    }
}
