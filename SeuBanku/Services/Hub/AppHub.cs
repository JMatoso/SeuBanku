#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SeuBanku.Entities;
using SeuBanku.Helpers;
using SeuBanku.Repositories;

namespace SeuBanku.Services.Hub
{
    public class AppHub : Hub<IAppHub>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericEntitiesRepo<Extract> _extractManager;
        private readonly IGenericEntitiesRepo<Service> _serviceManager;
        private readonly IGenericEntitiesRepo<Account> _accountManager;

        public AppHub(
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager,
            IGenericEntitiesRepo<Extract> extractManager, 
            IGenericEntitiesRepo<Account> accountManager,
            IGenericEntitiesRepo<Service> serviceManager)
        {
            _userManager = userManager;
            _extractManager = extractManager;
            _accountManager = accountManager;
            _serviceManager = serviceManager;
            _contextAccessor = contextAccessor;
        }

        [AllowAnonymous]
        [HubMethodName("GetAccountOwner")]
        public async Task GetAccountOwner(string accountNumber)
        {
            if(!string.IsNullOrEmpty(accountNumber))
            {
                var account = await _accountManager.GetObjectAsync(e => e.AccountNumber == Convert.ToInt32(accountNumber));

                if (account is null)
                {
                    await Clients.All.BadRequest("Account not found.");
                    return;
                }

                var user = await _userManager.FindByIdAsync(account.UserId.ToString());
                
                await Clients.All.Result(new
                {
                    OwnerName = user.FirstName + " " + user.LastName,
                    OwnerEmail = user.Email,
                    account.AccountNumber
                });

                return;
            }

            await Clients.All.BadRequest("Invalid reciever account number.");
        }

        [AllowAnonymous]
        [HubMethodName("Deposit")]
        public async Task Deposit(string accountNumber, string amount)
        {
            if (!string.IsNullOrEmpty(accountNumber) && !string.IsNullOrEmpty(amount))
            {
                var account = await _accountManager.GetObjectAsync(e => e.AccountNumber == Convert.ToInt32(accountNumber));

                if(account is not null)
                {
                    var dAmount = Convert.ToDecimal(amount);

                    if (dAmount > 0)
                    {
                        account.Balance += dAmount;

                        var user = await _userManager.FindByIdAsync(account.UserId.ToString());

                        if(user is not null)
                        {
                            var extract = new Extract()
                            {
                                InAccountNumber = account.AccountNumber,
                                OutcomingBalance = dAmount,
                                Operation = Models.BankOperations.Deposit,
                                From = "Deposit Service",
                                To = user.FirstName + " " + user.LastName,
                                IsApproved = true,
                                OperationDate = DateTime.Now,
                                Reference = ReferenceGenerator.GenerateNewReference(Models.BankOperations.Deposit)
                            };

                            await _accountManager.UpdateObjectAsync(account);

                            await _extractManager.InsertObjectAsync(extract);

                            await Clients.All.Successful("Operation performed!");
                            return;
                        }

                        await Clients.All.BadRequest("Account owner not found. Head to your bank!");
                        return;
                    }

                    await Clients.All.BadRequest("Insufficient balance.");
                    return;
                }

                await Clients.All.BadRequest("Account not found.");
                return;
            }

            await Clients.All.BadRequest("Invalid deposit data, account number or amount.");
            return;
        }

        [Authorize]
        [HubMethodName("Lifting")]
        public async Task Lifting(string amount)
        {
            if(!string.IsNullOrEmpty(amount))
            {
                var user = await GetUser();

                if(user is not null)
                {
                    var account = await _accountManager.GetObjectAsync(e => e.UserId == Guid.Parse(user.Id));

                    if(account is not null)
                    {
                        var dAmount = Convert.ToDecimal(amount);

                        if (account.Balance >= dAmount)
                        {
                            account.Balance -= dAmount;

                            var extract = new Extract()
                            {
                                OutAccountNumber = account.AccountNumber,
                                OutcomingBalance = dAmount,
                                From = user.FirstName + " " + user.LastName,
                                To = "Lift Service",
                                Operation = Models.BankOperations.Lift,
                                IsApproved = true,
                                OperationDate = DateTime.Now,
                                Reference = ReferenceGenerator.GenerateNewReference(Models.BankOperations.Lift)
                            };

                            await _accountManager.UpdateObjectAsync(account);

                            await _extractManager.InsertObjectAsync(extract);

                            await Clients.All.Successful("Operation performed!");
                            return;
                        }

                        await Clients.All.BadRequest("Insuficient balance.");
                        return;
                    }

                    await Clients.All.BadRequest("User account not found.");
                    return;
                }

                await Clients.All.BadRequest("User not found. Re-login!");
                return;
            }

            await Clients.All.BadRequest("Invalid amount.");
        }

        [Authorize]
        [HubMethodName("Payment")]
        public async Task Payment(string sid)
        {
            var user = await GetUser();

            if (user is not null)
            {
                if(!string.IsNullOrEmpty(sid))
                {
                    var service = await _serviceManager.GetObjectAsync(e => e.Id == Guid.Parse(sid));

                    if (service is not null)
                    {
                        var senderAccount = await _accountManager.GetObjectAsync(e => e.UserId == Guid.Parse(user.Id));

                        if (senderAccount is not null)
                        {
                            if (senderAccount.Balance >= service.Cost)
                            {
                                senderAccount.Balance -= service.Cost;

                                var extract = new Extract()
                                {
                                    OutAccountNumber = senderAccount.AccountNumber,
                                    InAccountNumber = service.AccountNumber,
                                    OutcomingBalance = service.Cost,
                                    From = user.FirstName + " " + user.LastName,
                                    To = service.ServiceName,
                                    Operation = Models.BankOperations.Payment,
                                    IsApproved = true,
                                    OperationDate = DateTime.Now,
                                    Reference = ReferenceGenerator.GenerateNewReference(Models.BankOperations.Payment)
                                };

                                await _accountManager.UpdateObjectAsync(senderAccount);

                                await _extractManager.InsertObjectAsync(extract);

                                await Clients.All.Successful("Operation performed!");
                                return;
                            }

                            await Clients.All.BadRequest("Insufficient balance.");
                            return;
                        }

                        await Clients.All.BadRequest("User account not found!");
                        return;
                    }

                    await Clients.All.BadRequest("Service not found!");
                    return;
                }

                await Clients.All.BadRequest("Fill all required fields!");
                return;
            }

            await Clients.All.BadRequest("Sender account not found. Re-login!");
        }

        [Authorize]
        [HubMethodName("Transfer")]
        public async Task Transfer(string accountNumber, string amount)
        {
            var user = await GetUser();

            if (user is not null)
            {
                if (!string.IsNullOrEmpty(accountNumber) && !string.IsNullOrEmpty(amount))
                {
                    var receiverAccount = await _accountManager.GetObjectAsync(e => e.AccountNumber == Convert.ToInt32(accountNumber));

                    if (receiverAccount is not null)
                    {
                        var senderAccount = await _accountManager.GetObjectAsync(e => e.UserId == Guid.Parse(user.Id));

                        if(senderAccount is not null)
                        {
                            var dAmount = Convert.ToDecimal(amount);

                            if(senderAccount.Balance >= dAmount)
                            {
                                senderAccount.Balance -= dAmount;
                                receiverAccount.Balance += dAmount;

                                var inUser = await _userManager.FindByIdAsync(receiverAccount.UserId.ToString());

                                var extract = new Extract()
                                {
                                    OutAccountNumber = senderAccount.AccountNumber,
                                    InAccountNumber = receiverAccount.AccountNumber,
                                    OutcomingBalance = dAmount,
                                    From = user.FirstName + " " + user.LastName,
                                    To = inUser.FirstName + " " + inUser.LastName,
                                    Operation = Models.BankOperations.Transfer,
                                    IsApproved = true,
                                    OperationDate = DateTime.Now,
                                    Reference = ReferenceGenerator.GenerateNewReference(Models.BankOperations.Transfer)
                                };

                                await _accountManager.UpdateObjectAsync(senderAccount);
                                await _accountManager.UpdateObjectAsync(receiverAccount);

                                await _extractManager.InsertObjectAsync(extract);

                                await Clients.All.Successful("Operation performed!");
                                return;
                            }

                            await Clients.All.BadRequest("Insufficient balance.");
                            return;
                        }

                        await Clients.All.BadRequest("Sender account not found.");
                        return;
                    }

                    await Clients.All.BadRequest("Reciever account not found.");
                    return;
                }

                await Clients.All.BadRequest("Invalid transfer data, account number or amount.");
                return;
            }

            await Clients.All.BadRequest("Sender account not found. Re-login!");
        }

        [Authorize]
        private async Task<ApplicationUser> GetUser()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            return user is null ? null : user;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await GetUser();

            if(user is not null)
            {
                await Groups.AddToGroupAsync(user.Id.ToString(), "BankHub");
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await GetUser();

            if (user is not null)
            {
                await Groups.RemoveFromGroupAsync(user.Id.ToString(), "BankHub");
                await base.OnDisconnectedAsync(exception);
            }
        }
    }
}
