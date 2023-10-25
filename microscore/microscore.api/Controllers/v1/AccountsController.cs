using Asp.Versioning;
using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.application.models.dtos.accounts;
using Microsoft.AspNetCore.Mvc;

namespace microscore.api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("/cuentas")]
    public class AccountsController : BaseApiController
    {
        private readonly IAccountServices _IAccountServices;

        public AccountsController(IAccountServices iAccountServices)
        {
            _IAccountServices = iAccountServices;
        }

        [HttpGet("buscarporcuenta/{number}")]
        public async Task<ActionResult<MsDtoResponse<AccountDTO>>> GetAccountbyNumbers(string number)
        {
            var result = await _IAccountServices.GetAccountByNumberAsync(number);
            return Ok(new MsDtoResponse<AccountDTO>(result, HttpContext.TraceIdentifier));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MsDtoResponse<AccountDTO>>> GetAccount(Guid id)
        {

            var account = await _IAccountServices.GetAccountByID(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(new MsDtoResponse<AccountDTO>(account, HttpContext.TraceIdentifier));
        }

        [HttpPost]
        public async Task<ActionResult<MsDtoResponse<AccountDTO>>> PostAccount(AccountRequest accountRequest)
        {
            var account = await _IAccountServices.CreateAccount(accountRequest);
            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        [HttpPut]
        public async Task<IActionResult> PutClient(AccountDTO account)
        {
            await _IAccountServices.UpdateAccountAsync(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {

            var client = await _IAccountServices.GetAccountByID(id);

            if (client == null)
            {
                return NotFound();
            }

            _ = await _IAccountServices.DeleteAccount(id);

            return NoContent();
        }
    }
}
