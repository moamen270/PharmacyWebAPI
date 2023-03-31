using AutoMapper;
using PharmacyWebAPI.Models.Dto;
using PharmacyWebAPI.Utility.Services.IServices;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISendGridEmail _sendGridEmail;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ISendGridEmail sendGridEmail)
        {
            _userManager = userManager;
            _sendGridEmail = sendGridEmail;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult GetRegister()
        {
            RegisterDto user = new RegisterDto();
            return Ok(new { User = user });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
                };
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { success = true, message = "User Registered Successfully  ", User = userDto });
                }
            }
            return BadRequest(new { success = false, message = " Registered Faild  ", User = userDto });
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult GetLogin()
        {
            LoginDto user = new LoginDto();
            return Ok(new { User = user });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password, true, false);
                if (result.Succeeded)
                    return Ok(new { success = true, message = "User Login Successfully  ", User = userDto });
                if (result.IsLockedOut)
                    return BadRequest("Account Locked Out");
            }
            return BadRequest(new { success = false, message = " Login Faild  ", User = userDto });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return Ok(new { ForgotPassword = new ForgotPasswordDto() });
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    return NotFound(new { success = false, message = "NotFound" });
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", values: new { userId = user.Id, code = code }, protocol: Request.Scheme);

                await _sendGridEmail.SendEmailAsync(model.Email, "Reset Email Confirmation", "Please reset email by going to this " +
                    "<a href=\"" + callbackurl + "\">link</a>");
                return Ok();
            }
            return BadRequest(model);
        }

        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string code)
        {
            return code is null ? BadRequest() : Ok();
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return BadRequest("Email Not Found");

                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest("Invalid Token");
            }
            return BadRequest(model);
        }
    }
}