using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SeuBanku.Entities;
using SeuBanku.Helpers;
using SeuBanku.Helpers.Document;
using SeuBanku.Helpers.Paging;
using SeuBanku.Models;
using SeuBanku.Repositories;

#nullable disable

namespace SeuBanku.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class BankController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericEntitiesRepo<Extract> _extractManager;
        private readonly IGenericEntitiesRepo<Service> _serviceManager;
        private readonly IGenericEntitiesRepo<Account> _accountManager;

        public BankController(
            UserManager<ApplicationUser> userManager, 
            IGenericEntitiesRepo<Extract> extractManager,
            IGenericEntitiesRepo<Service> serviceManager,
            IGenericEntitiesRepo<Account> accountManager)
        {
            _userManager = userManager;
            _serviceManager = serviceManager;
            _extractManager = extractManager;
            _accountManager = accountManager;
        }

        [Authorize]
        [Route("/")]
        [Route("/home")]
        [Route("/index")]
        [Route("/dashboard")]
        public async Task<IActionResult> Index() => View(await GetData());

        [Authorize, Route("/transfer")]
        public async Task<IActionResult> Transfer() => View(await GetData());

        [Authorize, Route("/payments")]
        public async Task<IActionResult> Payments() => View(await GetData());

        [Authorize, Route("/lifting")]
        public async Task<IActionResult> Lifting() => View(await GetData());

        [Authorize, Route("/activity")]
        public async Task<IActionResult> Activities([FromQuery]Parameters parameters)
        {
            var data = await GetData();

            var extracts = await _extractManager.GetObjectsAsync(parameters,
                e => e.OutAccountNumber == data.Account.AccountNumber || e.InAccountNumber == data.Account.AccountNumber);

            data.Extracts = extracts;
            data.Metadata = extracts.Metadata;

            return View(data);
        }

        [Authorize, Route("/account")]
        public async Task<IActionResult> Profile() => View(await GetData());

        [Authorize, Route("/invoice")]
        public async Task<IActionResult> Invoice(Guid invid)
        {
            var invoice = await _extractManager.GetObjectAsync(e => e.Id == invid);

            return invoice is not null ?
                File(new InvoiceDocument(invoice).GeneratePdf(), 
                    "application/pdf", ReferenceGenerator.GenerateDocumentName()) :
                RedirectToAction(nameof(NotFound));
        }

        [AllowAnonymous, Route("/deposit")]
        public IActionResult Deposit() => View();

        [HttpPost, AllowAnonymous, Route("/deposit")]
        public async Task<IActionResult> Deposit(string card, string amount)
        {
            if(ModelState.IsValid)
            {
                var account = await _accountManager.GetObjectAsync(a => a.AccountNumber == Convert.ToInt32(card.Replace("-", "")));

                if (account is not null)
                {
                    var user = await _userManager.FindByIdAsync(account.UserId.ToString());

                    if(user is not null)
                    {
                        var dAmount = Convert.ToDecimal(amount);

                        if (dAmount > 0)
                        {
                            account.Balance += dAmount;

                            var extract = new Extract()
                            {
                                InAccountNumber = account.AccountNumber,
                                OutcomingBalance = dAmount,
                                Operation = BankOperations.Deposit,
                                From = "Deposit Service",
                                To = user.FirstName + " " + user.LastName,
                                IsApproved = true,
                                OperationDate = DateTime.UtcNow,
                                Reference = ReferenceGenerator.GenerateNewReference(BankOperations.Deposit)
                            };

                            await _accountManager.UpdateObjectAsync(account);
                            await _extractManager.InsertObjectAsync(extract);

                            ViewBag.Ok = "Operation performed.";
                            return View();
                        }

                        ViewBag.Error = "Invalid amount.";
                        return View();
                    }

                    ViewBag.Error = "User not found.";
                    return View();
                }

                ViewBag.Error = "Account not found.";
                return View();
            }

            ViewBag.Error = "Fill all required fields.";
            return View();
        }

        private async Task<CommonModel> GetData()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null)
            {
                Redirect("/login");
                return null;
            }

            var account = await _accountManager.GetObjectAsync(a => a.UserId == Guid.Parse(user.Id));

            if (account is null)
            {
                RedirectToAction(nameof(NotFound));
                return null;
            }

            var common = new CommonModel()
            {
                User = user,
                Extracts = await _extractManager.GetObjectsAsync(e => e.OutAccountNumber == account.AccountNumber || e.InAccountNumber == account.AccountNumber),
                Services = await _serviceManager.GetObjectsAsync(),
                Account = account
            };

            return common;
        }

        [AllowAnonymous, Route("/notfound")]
        public new IActionResult NotFound() => View();

        [AllowAnonymous, Route("/forbidden")]
        public IActionResult Forbidden() => View();

        [AllowAnonymous, Route("/privacy")]
        public IActionResult Privacy() => View();

        [AllowAnonymous, Route("/error")]
        public IActionResult Error() => View();
    }
}