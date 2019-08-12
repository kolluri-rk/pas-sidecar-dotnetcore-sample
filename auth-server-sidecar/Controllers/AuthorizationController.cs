using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using auth_server_sidecar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace auth_server_sidecar.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(
            ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{appId:long}/users/{userId:long}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<string>>> Get(long appId, long userId)
        {
            _logger.LogInformation("serving request from sidecar");

            var user = await Task.FromResult(InMemoryAppUsersRepository.GetScopesForUserId(appId, userId));

            if (user == null) return NotFound();
            return Ok(user.Scopes);
        }

    }
}
