using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mofid.eWallet.Api.DTOs;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Security;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [TypeFilter(typeof(AuditRequestResponse))]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;
        private readonly IUtilityService utilityService;

        public UserController(IUserService userService, IJWTService jwtService, IUtilityService utilityService)
        {
            _userService = userService;
            _jwtService = jwtService;
            this.utilityService = utilityService;
        }


        /// <summary>
        /// ورود و دریافت توکن 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /auth
        ///	    {
        ///	       "username": "username",
        ///	       "password": "password",
        ///	    }
        ///     
        /// </remarks>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("user logged in successfully")</response>
        /// <response code="401">If wrong username and password</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("auth")]
        public async Task<ActionResult<ApiResponse>> Authenticate([FromBody] UserRequest userInfo)
        {

            (bool authenticated, User user) = await _userService.AuthenticateAsync(userInfo.Username, userInfo.Password);
            if (authenticated)
            {
                var token = _jwtService.GenerateToken("UserId", user.Id);
                return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new { token });
            }

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), StatusCodes.Status401Unauthorized, "login failed");
        }

      
    }
}