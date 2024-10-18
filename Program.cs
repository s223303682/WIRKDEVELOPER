using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models.sendemail;
using DinkToPdf;
using DinkToPdf.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Register DinkToPdf service
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Register EmailService
builder.Services.AddTransient<EmailService>();

// Get the connection string
var connectionString = builder.Configuration.GetConnectionString("ApplicationDBContextConnection")
    ?? throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");

// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

// Add Razor Pages support
builder.Services.AddRazorPages();

// Add MVC support
builder.Services.AddControllersWithViews();

var app = builder.Build(); // Build the app after all services are registered

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();
app.Run(); // Run the app

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager =
//        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    var roles = new[] { "Admin", "Nurse", "Pharmacist", "Surgeon","Anaesthesiologist"};

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//            await roleManager.CreateAsync(new IdentityRole(role));
//    }
//}
//using (var scope = app.Services.CreateScope())
//{
//    var userManager =
//        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    string firstName = "Mary";
//    string lastName = " Smith";
//    string email = "admin@gmail.com";
//    string password = "Admin@1";
//    bool confirmEmail = true;

//    if (await userManager.FindByEmailAsync(email) == null)
//    {
//        var User = new ApplicationUser();
//        User.FirstName = firstName; 
//        User.LastName = lastName;
//        User.UserName = email;
//        User.Email = email;
//        User.EmailConfirmed = confirmEmail;
//        await userManager.CreateAsync(User, password);
//        await userManager.AddToRoleAsync(User, "Admin");
//    }

//}
//using (var scope = app.Services.CreateScope())
//{
//    var userManager =
//        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    string firstName = "Amanda";
//    string lastName = "Lokhwe";
//    string email = "Nurse@gmail.com";
//    string password = "Nurse@1";
//    bool confirmEmail = true;

//    if (await userManager.FindByEmailAsync(email) == null)
//    {
//        var User = new ApplicationUser();
//        User.FirstName = firstName;
//        User.LastName = lastName;
//        User.UserName = email;
//        User.Email = email;
//        User.EmailConfirmed = confirmEmail;
//        await userManager.CreateAsync(User, password);
//        await userManager.AddToRoleAsync(User, "Nurse");
//    }

//}
//using (var scope = app.Services.CreateScope())
//{
//    var userManager =
//        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    string firstName = "Sipho";
//    string lastName = "Nzimande";
//    string email = "pharmacist@gmail.com";
//    string password = "Pharmacist@1";
//    bool confirmEmail = true;

//    if (await userManager.FindByEmailAsync(email) == null)
//    {
//        var User = new ApplicationUser();
//        User.FirstName = firstName;
//        User.LastName = lastName;
//        User.UserName = email;
//        User.Email = email;
//        User.EmailConfirmed = confirmEmail;
//        await userManager.CreateAsync(User, password);
//        await userManager.AddToRoleAsync(User, "Pharmacist");
//    }

//}
//using (var scope = app.Services.CreateScope())
//{
//    var userManager =
//        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    string firstName = "Emihle";
//    string lastName = "Banga";
//    string email = "surgeon@gmail.com";
//    string password = "Surgeon@1";
//    bool confirmEmail = true;

//    if (await userManager.FindByEmailAsync(email) == null)
//    {
//        var User = new ApplicationUser();
//        User.FirstName = firstName;
//        User.LastName = lastName;
//        User.UserName = email;
//        User.Email = email;
//        User.EmailConfirmed = confirmEmail;
//        await userManager.CreateAsync(User, password);
//        await userManager.AddToRoleAsync(User, "Surgeon");
//    }

//}
//using (var scope = app.Services.CreateScope())
//{
//	var userManager =
//		scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//	string firstName = "John";
//	string lastName = "Doe";
//	string email = "Anaesthesiologist@gmail.com";
//	string password = "Anaesthesiologist@1";
//	bool confirmEmail = true;

//	if (await userManager.FindByEmailAsync(email) == null)
//	{
//		var User = new ApplicationUser();
//		User.FirstName = firstName;
//		User.LastName = lastName;
//		User.UserName = email;
//		User.Email = email;
//		User.EmailConfirmed = confirmEmail;
//		await userManager.CreateAsync(User, password);
//		await userManager.AddToRoleAsync(User, "Anaesthesiologist");
//	}

//}

