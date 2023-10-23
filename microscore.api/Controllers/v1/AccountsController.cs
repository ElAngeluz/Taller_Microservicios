using Asp.Versioning;
using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.application.models.dtos.accounts;
using Microsoft.AspNetCore.Mvc;

namespace microscore.api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("/cuenta")]
    public class AccountsController : BaseApiController
    {
        private readonly IAccountServices _IAccountServices;

        [HttpGet("{number}")]
        public async Task<ActionResult<MsDtoResponse<AccountDTO>>> GetAccountbyNumbers(string number)
        {
            return Ok();         
        }
    }
}
