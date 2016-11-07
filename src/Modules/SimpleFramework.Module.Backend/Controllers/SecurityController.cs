using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Web.SmartTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using SimpleFramework.Core.Security;

namespace SimpleFramework.Module.Backend.Controllers
{

    [Authorize(Roles = "admin")]
    [Route("api/users")]
    public class SecurityController : Core.Web.Base.Controllers.ControllerBase
    {

        private readonly ISecurityService _securityService;
        public SecurityController(
            ISecurityService securityService,
            IServiceCollection service,
            ILogger<UserApiController> logger) : base(service, logger)
        {

            this._securityService = securityService;
        }
        
    }
}
