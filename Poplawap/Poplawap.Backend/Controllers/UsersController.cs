using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Poplawap.Backend.Helpers;
using Poplawap.Backend.Infrastructure;
using Poplawap.Backend.Model;
using Poplawap.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Poplawap.Backend.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ICipherService _cipherService;

        /// <summary>
        /// UsersController's constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="appSettings"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="cipherService"></param>
        public UsersController(ApplicationDbContext db, IOptions<AppSettings> appSettings,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, ICipherService cipherService)
        {
            _db = db;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cipherService = cipherService;
        }

        /// <summary>
        /// Authorize a specific user with the email and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateUserDTO user)
        {
            
            if (user == null)
            {
                return BadRequest(Utils.GetResponse("EmptyUser", "The user is null"));
            }

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

            if (!result.Succeeded)
            {
                return BadRequest(Utils.GetResponse("IncorrectCredentials", "Email or password are incorrect"));
            }                

            var token = BuildToken(user.Email);

            return Ok(Utils.GetResponse("Token", token));
        }

        /// <summary>
        /// Logout the user logged
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest(Utils.GetResponse("EmptyUser", "The user is null"));
            }

            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Verified = user.Verified,
                County = user.County,
                Password = _cipherService.Encrypt(user.Password)
            };

            var result = await _userManager.CreateAsync(appUser, user.Password);

            if (!result.Succeeded)
            {               
                return BadRequest(Utils.GetResponseByErrorList(result.Errors));
            }

            string code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //Utils.SendEmail(appUser.Email, code);

            var token = BuildToken(user.Email);

            return Ok(Utils.GetResponse("Token", token));
        }

        /// <summary>
        /// Updates user information
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDTO user)
        {
            if (user == null)
                return BadRequest(Utils.GetResponse("EmptyUser", "The user is null"));

            ApplicationUser appUser = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                County = user.County
            };

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                return BadRequest(Utils.GetResponseByErrorList(result.Errors));
            }

            return Ok();
        }

        private string BuildToken(string email)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}