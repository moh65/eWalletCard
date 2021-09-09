using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;

namespace Mofid.eWallet.Api.Controllers
{
	[AuthorizedToken]
	[TypeFilter(typeof(AuditRequestResponse))]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TemporaryClientController : ControllerBase
    {
        private readonly ITemporaryClientService temporaryClientService;
        private readonly IUtilityService utilityService;

        public TemporaryClientController(ITemporaryClientService temporaryClientService, IUtilityService utilityService)
        {
            this.temporaryClientService = temporaryClientService;
            this.utilityService = utilityService;
        }

        /// <summary>
        /// import whitelist از اکسل
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    POST /ImportExcel
        ///	    {
        ///	       "WhiteList": "file binary",
        ///	    }
        ///     
        /// </remarks>
        /// <param name="WhiteList"> فایل اکسل </param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("clients have been imported Successfully")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("ImportExcel")]
        public async Task<ActionResult<ApiResponse>> ImportExcel(IFormFile WhiteList)
        {
            await temporaryClientService.Import(WhiteList);

            return new ApiResponse(utilityService.GetCurrentRefCodePerScope(), "clients have been imported Successfully");

        }


        /// <summary>
        ///لیست کاربران وایت لیست
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /List    
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("clients have been imported Successfully")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("List")]
        public async Task<ActionResult<ApiResponse>> List([FromQuery]ClientListRequest request)
        {
            var data = await temporaryClientService.List(request.Skip , request.Take , request.NationalCode , request.Mobile);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new  {
                data= data.Item2,
                total = data.Item1
            });

        }


        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /List    
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the success message("clients have been imported Successfully")</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Delete")]
        public async Task<ActionResult<ApiResponse>> Delete([FromBody] ClientDeleteRequest request)
        {
            await temporaryClientService.Delete(request.Id);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), true);

        }
    }
}
