using AutoMapper;
using AutoMapper.Execution;
using Internet_Market_WebApi.Abstract;
using Internet_Market_WebApi.Constants;
using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Internet_Market_WebApi.Models;
using Internet_Market_WebApi.Services.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tsp;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Internet_Market_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISmtpEmailService _emailService;
        private readonly IConfiguration _configuration;
        public UserController(IJwtTokenService jwtTokenService,
            UserManager<UserEntity> userManager,
            ISmtpEmailService emailService,
            IConfiguration configuration)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isPasswordValid)
                {
                    return BadRequest();

                }
                var token = _jwtTokenService.CreateToken(user);
                return Ok(new { token });
            }
            return BadRequest();
        }
        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Password unconfirmed");
            }
            UserEntity user = new UserEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
            };
            try
            {
                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    result = _userManager.AddToRoleAsync(user, Roles.User).Result;
                    //Sending mail to confirm account
                    //
                    //
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var myUrl = @"http://localhost:3000";

                    var callbackUrl = $"{myUrl}/accountConfirm?userId={user.Id}&" +
                        $"code={WebUtility.UrlEncode(token)}";

                    var message = new Message()
                    {
                        To = user.Email,
                        Subject = "Confirm Account",
                        Body = "To confirm account click reference below:" +
                            $"<a href='{callbackUrl}'>Confirm</a>"
                    };
                    _emailService.Send(message);
                    //
                    //
                    //
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();

            }

        }
        [HttpPost("/GetUser")]
        public async Task<IActionResult> GetUser(JwtTokenViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Data);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                return Ok(new
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                });
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                user.Email = model.User.Email;
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                await _userManager.UpdateAsync(user);
                var token = _jwtTokenService.CreateToken(user);
                return Ok(new { token });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var frontendUrl = _configuration.GetValue<string>("FrontEndURL");

            var myUrl = @"http://localhost:3000";

            var callbackUrl = $"{myUrl}/resetpassword?userId={user.Id}&" +
                $"code={WebUtility.UrlEncode(token)}";

            var message = new Message()
            {
                To = user.Email,
                Subject = "Reset Password",
                Body = "To reset password click reference below:" +
                    $"<a href='{callbackUrl}'>Reset Password</a>"
            };
            _emailService.Send(message);
            return Ok();
        }
        [HttpPost("/changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var res = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/confirmAccount")]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccountModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                await _userManager.ConfirmEmailAsync(user, model.Token);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("/MakePayment")]
        public async Task<IActionResult> MakePayment(PaymentFormModel model)
        {
            try
            {
                ////DO SOME
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
        
