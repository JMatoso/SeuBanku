using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuBanku.Data;
using SeuBanku.Entities;
using SeuBanku.Helpers;
using SeuBanku.Models.Request;
using SeuBanku.Options;
using SeuBanku.Repositories;
using System.Security.Claims;

#nullable disable

namespace SeuBanku.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AuthController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGenericEntitiesRepo<Extract> _extractManager;
        private readonly IGenericEntitiesRepo<Account> _accountManager;

        public AuthController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IGenericEntitiesRepo<Extract> extractManager,
            IGenericEntitiesRepo<Account> accountManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _extractManager = extractManager;
            _accountManager = accountManager;
        }

        [AllowAnonymous]
        [Route("/login")]
        public IActionResult Login(string returnUrl = "/")
        {
            return User.Identity.IsAuthenticated ? RedirectToAction("index", "bank") : 
                View(new Login { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/login")]
        public async Task<IActionResult> Login(Login login)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if(user is not null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);

                    if (result.Succeeded)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Email),
                            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                            new Claim(ClaimTypes.Role, user.Role)
                        };

                        var identity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        User.AddIdentity(identity);

                        await _signInManager.SignInAsync(user, login.RememberMe, CookieAuthenticationDefaults.AuthenticationScheme);

                        return !string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl) ?
                            Redirect(login.ReturnUrl) :
                            RedirectToAction("index", "bank");
                    }

                    if(result.IsLockedOut)
                    {
                        ViewBag.Error = "User is temporary locked out.";
                        return View();
                    }
                }

                ViewBag.Error = "Wrong credentials.";
                return View();
            }

            ViewBag.Error = "Fill all required fields.";
            return View();
        }

        [AllowAnonymous]
        [Route("/signin")]
        public IActionResult Signin() =>  User.Identity.IsAuthenticated ? RedirectToAction("index", "bank") : View();

        [HttpPost]
        [AllowAnonymous]
        [Route("/signin")]
        public async Task<IActionResult> Signin(Signin signin)
        {
            if(ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(u => u.Email == signin.Email))
                {
                    ViewBag.Error = "Email already taken!";
                    return View();
                }

                var uid = Guid.NewGuid();
                var user = new ApplicationUser()
                {
                    Id = uid.ToString(),
                    FirstName = signin.FirstName.Trim(),
                    LastName = signin.LastName.Trim(),
                    Email = signin.Email.Trim(),
                    NormalizedEmail = signin.Email.Trim().ToUpper(),
                    UserName = signin.Email.Trim(),
                    NormalizedUserName = signin.Email.Trim().ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Role = nameof(ServiceRolesEnumerator.ROLE_CLIENT)                    
                };

                await _roleManager.ApplyRole();

                var result = await _userManager.CreateAsync(user, signin.Password);

                if (!result.Succeeded)
                {
                    ViewBag.Error = "Something went wrong, try again!";
                    return View();
                }

                await _userManager.AddToRoleAsync(user, user.Role);

                var account = new Account()
                {
                    AccountNumber = new Random().Next(100000000, 999999999),
                    UserId = Guid.Parse(user.Id),
                    Balance = signin.IntialBalance,
                    Limit = signin.DiaryLimit,
                    PeriodToTakeMoneyInYears = signin.PeriodToTakeMoneyInYears,
                    IsDisabled = false,
                    ExpireDate = DateTime.UtcNow.AddYears(10),
                    OpenedDate = DateTime.UtcNow,
                    AccountType = signin.AccountType,
                };

                await _accountManager.InsertObjectAsync(account, x => x.AccountNumber == account.AccountNumber);

                var extract = new Extract()
                {
                    InAccountNumber = account.AccountNumber,
                    OutcomingBalance = signin.IntialBalance,
                    Operation = Models.BankOperations.Deposit,
                    From = "Account Opening",
                    To = user.FirstName + " " + user.LastName,
                    IsApproved = true,
                    OperationDate = DateTime.UtcNow,
                    Reference = ReferenceGenerator.GenerateNewReference(Models.BankOperations.Deposit)
                };

                await _extractManager.InsertObjectAsync(extract);

                return await Login(new Login
                {
                    Email = user.Email,
                    Password = signin.Password,
                    ReturnUrl = "/",
                    RememberMe = false
                });
            }

            ViewBag.Error = "Fill all required fields.";
            return View();
        }

        [Authorize]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
