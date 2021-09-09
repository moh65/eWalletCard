using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.DTOs;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;
using Mofid.eWallet.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Controllers
{
    [AuthorizedToken]
    [TypeFilter(typeof(AuditRequestResponse))]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    
    public class ClientController : ControllerBase
    {
        private readonly IClientKycService clientKycService;
        private readonly IUtilityService utilityService;

        public ClientController(IClientKycService clientKycService, IUtilityService utilityService)
        {
            this.clientKycService = clientKycService;
            this.utilityService = utilityService;
        }

        /// <summary>
        /// ثبت نام مشتری برای دریافت مفید کارت.
        /// مشتری باید قبلا در tbs ثبت نام شده باشد.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /kyc
        ///	    {
        ///	       "nationalCode": "1234567890",
        ///	       "nationalCardSerial": "1234567",
        ///	       "phoneNumber": "09123456789",
        ///	       "deviceId": "112344124",
        ///	       "postalCode": "1234567891",
        ///	       "address": "تهران..."
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("otp sent successfuly")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("kyc")]
        public async Task<ActionResult<ApiResponse>> Kyc([FromBody] KycRequest request)
        {
            await clientKycService.KycAsync(request.ToClient(), request.ToToken());
            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "otp sent successfuly") { };
        }


        /// <summary>
        /// اعتبار سنجی شماره رمز یکبار مصرف ارسال شده به مشتری
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /verify
        ///	    {
        ///	       "deviceId": "112344124",
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
        [HttpPost("verify")]
        public async Task<ActionResult<ApiResponse>> Verify([FromBody] VerifyRequest request)
        {
            await clientKycService.Verify(request.ToClient(), request.ToToken(), request.Otp);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "client verified successfully");

        }



        /// <summary>
        /// دریافت اطلاعات آدرس و کد پستی مشتری از کد ملی
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /Details
        ///	    {
        ///	       "nationalCode": "nationalCode",
        ///	    }
        ///     
        /// </remarks>
        /// <response code="200">Returns data { address  : *** , nationalCode: *** , addressCity : **** }</response>
        /// <response code="404">national code not in tbs</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Address")]
        public async Task<ActionResult<ApiResponse>> Address([FromQuery] AddressClientRequest request)
        {
            Client result = await clientKycService.GetAddress(request.MapToClient());

            if (result == null)
                return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), StatusCodes.Status404NotFound);

            var address = result.Addresses.FirstOrDefault();

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new
            {
                address = address.AddressString,
                postalCode = address.Postalcode,
                addressCity = address.City,
                birthDate = result.BirthDate,
            });
        }

        /// <summary>
        /// تایید فیزیکی برای دریافت کارت
        /// بعد از ثبت نام کاربر و پرداخت هزینه فراخوانی میشود.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /physicalVerify
        ///	    {
        ///	       "nationalCode": "1234567890"
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("client physicaly verified successfully")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("physicalVerify")]
        public async Task<ActionResult<ApiResponse>> PhysicalVerify([FromBody] PhysicalVerifyRequest request)
        {
            await clientKycService.PhysicalVerifing(request.NationalCode);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "client physicaly verified successfully");

        }
        /// <summary>
        /// دریافت مانده حساب از tbs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /TbsRemain?nationalCode=**********
        ///     
        /// </remarks>
        /// <response code="200"> Returns data { remain  : *** } </response>
        /// <response code="500">If ocure any error</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("TbsRemain")]
        public async Task<ActionResult<ApiResponse>> TbsRemain([FromQuery] TbsRemainsRequest request)
        {
            var result = await clientKycService.GetTbsRemain(request.NationalCode);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new
            {
                remain = result.CurrentRemain,
            });
        }

        /// <summary>
        /// دریافت مانده حساب از بانک
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /BankRemain?nationalCode=**********
        ///     
        /// </remarks>
        /// <response code="200"> Returns data { remain  : *** } </response>
        /// <response code="500">If ocure any error</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("BankRemain")]
        public async Task<ActionResult<ApiResponse>> BankRemain([FromQuery] BankRemainsRequest request)
        {
            var result = await clientKycService.GetBankRemain(request.NationalCode , request.DeviceId);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new
            {
                remain = result.CurrentRemain,
            });
        }


        /// <summary>
        /// چک کردن کاربر در whitelist جهت پیغام مفید کارت
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /IsLegal?nationalCode=1234567890
        ///     
        /// </remarks>
        /// <response code="200"> Returns data true or falsee </response>
        /// <response code="500">If ocure any error</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("IsLegal")]
        public async Task<ActionResult<ApiResponse>> IsLegal([FromQuery] WhiteListRequest request)
        {
            bool result = await clientKycService.IsLegal(request.NationalCode);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), result);
        }

        /// <summary>
        /// دریافت وضعیت فرایند صدور کارت برای مشتری
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /clientStates
        ///	    {
        ///	       "nationalCode": "1234567890"
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the client's states</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("clientStates")]
        public async Task<ActionResult<ApiResponse>> ClientStates([FromQuery] ClientStatesRequest request)
        {
            List<ClientState> result = await clientKycService.GetClientStates(request.NationalCode);
            var resultString = result.Select(a => a.ToString()).ToList();
            return new ApiResponse<List<string>>(utilityService.GetCurrentRefCodePerScope(), resultString);

        }


        /// <summary>
        /// لیست مشتریان
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /list
        ///	    {
        ///	       "nationalCode": "",
        ///	       "mobile": ""
        ///	       "skip": "0"
        ///	       "skip": "20"
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns clients</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse>> list([FromQuery] ClientListRequest request)
        {
            var result = await clientKycService.ListAsync(request.Skip , request.Take , request.NationalCode , request.Mobile );

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new { 
                data = result.Item2,
                total = result.Item1
            });

        }
        /// <summary>
        /// اطلاعات مشتری
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /info
        ///	    {
        ///	       "nationalCode": "",
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns client's info</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("info")]
        public async Task<ActionResult<ApiResponse>> info([FromQuery] ClientInfoRequest request)
        {
            var result = await clientKycService.GetClientAsync(request.NationalCode);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), result);

        }
    }
}