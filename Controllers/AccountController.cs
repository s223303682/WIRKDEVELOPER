using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using WIRKDEVELOPER.Models;
using WIRKDEVELOPER.Models.Account;
using Microsoft.AspNetCore.Authorization;

namespace WIRKDEVELOPER.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _dbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        //Users
        public IActionResult Users()
        {
            var AdminUsers = _dbContext.Users.Where(r => r.Role == "Admin").Count();

            var NurseUsers = _dbContext.Users.Where(r => r.Role == "Nurse").Count();

            var AnaUsers = _dbContext.Users.Where(r => r.Role == "Anaesthesiologist").Count();

            var SurgUsers = _dbContext.Users.Where(r => r.Role == "Surgeon").Count();

            var PharmUsers = _dbContext.Users.Where(r => r.Role == "Pharmacist").Count();

            var model = new UesrViewModel
            {
                NurseUser = NurseUsers,
                AnaUser = AnaUsers,
                SurgUser = SurgUsers,
                PharmUser = PharmUsers,
                AdminUser = AdminUsers
            };



            
            return  View(model);
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var role = (await _userManager.GetRolesAsync(user))[0];

            var model = new UserViewModel
            {
                Lastname = user.LastName,
                Role = role,
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Register(string role)
        {
            var model = new RegisterViewModel { Role = role };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ApplicationUser user;
            if (model.Role == "Admin")
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    number = model.ContactNumber,
                    Role = model.Role,
                };

                var administrator = new Models.Account.Admin
                {
                    ApplicationUser = user,
                };
                _dbContext.Administrators.Add(administrator);
            }
            else if (model.Role == "Surgeon")// For Healthcare Professionals
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    number = model.ContactNumber,
                    Role = model.Role,
                };

                var surgeon = new Surgeon
                {
                    SurgeonLicenseNumber = model.HPCSANumber,
                    Specialization = model.Specialization,
                    ApplicationUser = user
                };

                _dbContext.Surgeons.Add(surgeon);
            }
            else if (model.Role == "Pharmacist")
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    number = model.ContactNumber,
                    Role = model.Role,
                };

                var pharmacist = new Pharmacist
                {
                    PharmacyLicenseNumber = model.PharmacyLicenseNumber,
                    ApplicationUser = user
                };

                _dbContext.Pharmacists.Add(pharmacist);
            }
            else if(model.Role == "Nurse")
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    number = model.ContactNumber,
                    Role = model.Role,
                };

                var nurse = new Nurse
                {
                    NurseLicenseNumber = model.NurseLicenseNumber,
                    ApplicationUser = user
                };

                _dbContext.Nurses.Add(nurse);
            }
            else 
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    number = model.ContactNumber,
                    Role = model.Role,
                };

                var ana = new Anaesthesiologist
                {
                    AnaesthesiologistLicenseNumber = model.AnaesthesiologistLicenseNumber,
                    ApplicationUser = user
                };

                _dbContext.Anaesthesiologists.Add(ana);
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await EnsureRoleExistsAsync(model.Role);
                await _userManager.AddToRoleAsync(user, model.Role);
                TempData["SuccessMessage"] = $"{model.Role} has been registered successfully!";
                return RedirectToAction("Users", "Account");
            }
            AddErrors(result);

            return View(model);

        }
        private async Task EnsureRoleExistsAsync(string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                   

                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("AdminDashboard", "Admin");
                        }
                        if (roles.Contains("Pharmacist"))
                        {
                            return RedirectToAction("Dashboard", "Pharmacist");
                        }
                        if (roles.Contains("Surgeon"))
                        {
                            return RedirectToAction("DashBoard", "Surgeon");
                        }
                        if (roles.Contains("Anaesthesiologist"))
                        {
                            return RedirectToAction("Anaesthesiologist", "Anaesthesiologist");
                        }

                        return RedirectToLocal(returnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        return View("Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }
    }
}
