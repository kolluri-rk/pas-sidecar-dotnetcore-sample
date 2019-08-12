using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sidecar_dependent_app.Models;
using sidecar_dependent_app.Services;

namespace sidecar_dependent_app.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IAppAutherizationService _appAutherizationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IAppAutherizationService appAutherizationService, 
            ILogger<HomeController> logger)
        {
            _appAutherizationService = appAutherizationService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> App1()
        {
            var vm = new UserAuthViewModel() { Scopes = await GetScopes(1, 1) } ;
            
            return View(vm);
        }

        public async Task<IActionResult> App2()
        {
            var vm = new UserAuthViewModel() { Scopes = await GetScopes(2, 1) } ;
            
            return View(vm);
        }

        public async Task<IActionResult> App3()
        {
            var vm = new UserAuthViewModel() { Scopes = await GetScopes(3, 1) } ;
            
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private async Task<IEnumerable<string>> GetScopes(long appId, long userId)
        {
            // var scopes = new List<string>();

            // try
            // {
            //     //var scopes = new string[] {"admin", "scope1"};
            //     scopes = await _appAutherizationService.GetAthenticatedUserScopesAsync(appId, userId);
            //     if(scopes == null) return Enumerable.Empty<string>();
            // }
            // catch(Exception ex)
            // {
            //     Error();
            // }

            // return scopes;

            //var scopes = new string[] {"admin", "scope1"};
            var scopes = await _appAutherizationService.GetAthenticatedUserScopesAsync(appId, userId);
            return scopes;
        }
    }
}
