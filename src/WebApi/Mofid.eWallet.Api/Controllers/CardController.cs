using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services.Contracts;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Controllers
{
    [AuthorizedToken]
    [TypeFilter(typeof(AuditRequestResponse))]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;
        private readonly IUtilityService utilityService;

        public CardController(ICardService cardService, IUtilityService utilityService)
        {
            this.cardService = cardService;
            this.utilityService = utilityService;
        }

        /// <summary>
        ///  که ثبت نام مشتری برای دریافت مفید کارت
        /// مشتری باید قبلا در tbs ثبت نام شده باشد.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /register
        ///	    {
        ///	       "nationalCode": "1234567890",
        ///	       "phoneNumber": "09123456789",
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("card otp sent successfuly")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] CardRegisterRequest request)
        {
            await cardService.RegisterCardAsync(request.NationalCode, request.PhoneNumber);
            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "card otp sent successfuly") { };
        }
        /// <summary>
        /// فعال سازی کارت با شماره کارت بانکی 
        /// برای کاربر باید قبلا درخواست مفید کارت ثبت شده باشد.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /activate
        ///	    {
        ///	       "nationalCode": "112344124",
        ///	       "phoneNumber": "09123456789",
        ///	       "pan":"1234567890123456"
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("client verified successfully")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("activate")]
        public async Task<ActionResult<ApiResponse>> Activate([FromBody] CardActivateRequest request)
        {
            await cardService.ActivateAsync(request.NationalCode, request.PhoneNumber, request.Pan);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "card activated successfully");
        }

        /// <summary>
        /// فعال سازی کارت با رمز یکبار مصرف ارسال شده به کاربر و شماره کارت بانکی
        /// برای کاربر باید قبلا درخواست مفید کارت ثبت شده باشد.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /verifyactivate
        ///	    {
        ///	       "nationalCode": "112344124",
        ///	       "phoneNumber": "09123456789",
        ///	       "otp":"123456"
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("client verified successfully")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("verifyactivate")]
        public async Task<ActionResult<ApiResponse>> VerifyActivate([FromBody] VerifyCardActivateRequest request)
        {
            await cardService.ActivateAsync(request.NationalCode, request.PhoneNumber, request.Otp);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "card activated successfully");
        }
    }
}