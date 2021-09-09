using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mofid.eWallet.Api.DTOs;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services.Contracts;

namespace Mofid.eWallet.Api.Controllers
{
    [AuthorizedToken]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [TypeFilter(typeof(AuditRequestResponse))]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IUtilityService utilityService;

        public PaymentController(IPaymentService paymentService, IUtilityService utilityService)
        {
            this.paymentService = paymentService;
            this.utilityService = utilityService;
        }



        /// <summary>
        /// ورود و دریافت توکن 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /TbsToCard
        ///	    {
        ///	       "nationalCode": "1234567890" ,
        ///	       "amount": "100000" ریال,
        ///	    }
        ///     
        /// </remarks>
        /// <param name="transfer"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("successfully trnasfered.")</response>
        /// <response code="401">If wrong username and password</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("TbsToCard")]
        public async Task<ActionResult<ApiResponse>> TbsToCard([FromBody] TbsToCardRequest transfer)
        {
            await paymentService.TbsToCard(transfer.NationalCode , transfer.Amount);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(),"successfully trnasfered.");
        }

        /// <summary>
        /// انتقال وجه از کارت به کارگزاری 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /CardToTbs
        ///	    {
        ///	       "nationalCode": "1234567890" ,
        ///	       "amount": "100000" ریال,
        ///	    }
        ///     
        /// </remarks>
        /// <param name="transfer"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("successfully trnasfered.")</response>
        /// <response code="401">If wrong username and password</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("CardToTbs")]
        public async Task<ActionResult<ApiResponse>> CardToTbs([FromBody] TbsToCardRequest transfer)
        {
            await paymentService.CardToTbs(transfer.NationalCode, transfer.Amount);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "successfully trnasfered.");
        }
    }
}
