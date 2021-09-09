using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Infra.ElasticSearch;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;

namespace Mofid.eWallet.Api.Controllers
{
	[AuthorizedToken]
	[TypeFilter(typeof(AuditRequestResponse))]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ElasticLogger<Logstash> elastic;
        private readonly IUtilityService utilityService;
        public LogController(ElasticLogger<Logstash> elastic , IUtilityService utilityService)
        {
            this.utilityService = utilityService;
            this.elastic = elastic;
        }


        /// <summary>
        /// دریافت اطلاعات لاگ ها
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///	    GET /list
        ///	    {
        ///	       "skip": "0"
        ///	       "take": "20"
        ///	    }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the pod logs</response>
        /// <response code="500">If ocure any error</response>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("List")]
        public async Task<ActionResult<ApiResponse>> List([FromQuery] LogListRequest request)
        {
            var result = await elastic.TaskAsync(request.Skip , request.Take  , request.NationalCode);

            return new ApiResponse<object>(utilityService.GetCurrentRefCodePerScope(), new { 
                    total = result.Item1,
                    data = result.Item2,
            });

        }
    }
}
