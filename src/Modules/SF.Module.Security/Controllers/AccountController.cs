using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SimpleFramework.Module.Security.ViewModels.Account;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Services;
using Audit.Mvc;

namespace SimpleFramework.Module.Security.Controllers
{
    [Area("Backend")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        //
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

      
      
    }
}
