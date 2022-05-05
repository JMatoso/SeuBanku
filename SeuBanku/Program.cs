using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuBanku.Data;
using SeuBanku.Entities;
using SeuBanku.Repositories;
using SeuBanku.Services.Hub;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Production
});

builder.Services.AddControllersWithViews(configure => {
    configure.RespectBrowserAcceptHeader = true;
    configure.ReturnHttpNotAcceptable = true;
}).AddRazorRuntimeCompilation();

builder.Services
        .AddScoped<IGenericEntitiesRepo<Account>, GenericEntitiesRepo<Account>>()
        .AddScoped<IGenericEntitiesRepo<Extract>, GenericEntitiesRepo<Extract>>()
        .AddScoped<IGenericEntitiesRepo<Service>, GenericEntitiesRepo<Service>>()
        .AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR().AddMessagePackProtocol();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddResponseCaching()
                .AddMemoryCache();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/forbidden";
    options.Cookie.Name = "SeuBankuCookieAuth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/login";
    options.ReturnUrlParameter = "returnUrl";
    options.SlidingExpiration = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc(options => {
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Required field.");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/bank/error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(opt => {
    opt.AllowAnyHeader();
    opt.AllowAnyHeader();
    opt.SetIsOriginAllowed((host) => true);
    opt.AllowCredentials();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=bank}/{action=index}/{id?}");

app.MapHub<AppHub>("/bankHub");

app.Run();
