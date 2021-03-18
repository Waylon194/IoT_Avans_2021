using IWSN_Backend_Server.Models;
using IWSN_Backend_Server.Models.Settings.Class;
using IWSN_Backend_Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Controllers
{
    // This is the url route of this controller, this must match with the full route name
    [Route( DBRouteSettings.AccountMainRouteName )] // consisting of api/[VERSION]  [ROUTE-URL OF ACCOUNT]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountService _AccountService;

        public BankAccountController(BankAccountService service)
        {
            // assign the service to the class variable
            this._AccountService = service;
        }

        // get all the available users - async
        [Route( DBRouteSettings.AccountSubRouteName + "/all" )]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDBModel>>> GetAllUsers()
        {
            var users = await this._AccountService.GetAllAccountsAsync();
            return users.ToList();
        }

        // get a single user by MongoDB assign Id
        [Route(DBRouteSettings.AccountSubRouteName + "/{id}")]
        [HttpGet]
        public async Task<ActionResult<AccountDBModel>> GetById(string id)
        {
            var user = await this._AccountService.GetAccountByIdAsync(id);
            return user;
        }

        // Create a single user document in MongoDB - works but throws an error? -> System.InvalidOperationException: No route matches the supplied values.
        [Route(DBRouteSettings.AccountSubRouteName + "/create")]
        [HttpPost]
        public async Task<ActionResult<AccountDBModel>> CreateUser([FromBody]AccountDBModel user)
        {
            var userCreated = await _AccountService.CreateAccountAsync(user);
            return userCreated;
        }

        // 
        [Route(DBRouteSettings.AccountSubRouteName + "/update/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody]AccountDBModel user)
        {
            var gatheredUser = await this._AccountService.GetAccountByIdAsync(id);

            if (gatheredUser == null)
            {
                return NotFound();
            }
            await this._AccountService.UpdateAccountAsync(id, user);
            return NoContent();
        }
        
        //
        [Route(DBRouteSettings.AccountSubRouteName + "/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var accountTaskObject = await this._AccountService.GetAccountByIdAsync(id);

            if (accountTaskObject == null)
            {
                return NotFound();
            }
            await this._AccountService.RemoveAccountAsync(id);

            return NoContent();
        }
    }
}
