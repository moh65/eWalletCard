using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Controllers
{
    [AuthorizedToken]
    [TypeFilter(typeof(AuditRequestResponse))]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<TokenController> _logger;
        private readonly IUtilityService utilityService;

        public TokenController(ITokenService tokenService, ILogger<TokenController> logger, IUtilityService utilityService)
        {
            _tokenService = tokenService;
            _logger = logger;
            this.utilityService = utilityService;
        }

        /// <summary>
        /// دریافت access token
        /// برای فراخوانی باید قبلا لاگین کرده باشید
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /token
        ///	    {
        ///	       "phoneNumber": "09123456789",
        ///	       "deviceId": "112344124",
        ///	    }
        ///     
        /// </remarks>
        /// <param name="acquire"></param>
        /// <returns></returns>
        /// <response code="200">retrun the access token string</response>
        /// <response code="500">if ocure any internal error</response>
        /// <response code="401">unauthorize</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Acquire([FromBody] AcquireRequest acquire)
        {
            var token = await _tokenService.AcquireTokenAsync(acquire.PhoneNumber, acquire.DeviceId);
            if (token != null)
            {
                return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new { token.AccessToken });
            }
            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), StatusCodes.Status401Unauthorized, "token not found");

        }
    }



}