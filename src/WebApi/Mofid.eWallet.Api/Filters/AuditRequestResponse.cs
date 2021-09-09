using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Mofid.eWallet.Infra.ElasticSearch;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Filters
{
    public class AuditRequestResponse : AuditFilterAttribute
    {
        private readonly ILogger<AuditRequestResponse> logger;
        public AuditRequestResponse(ILogger<AuditRequestResponse> logger, IUtilityService utilityService)
            : base(utilityService)
        {
            this.logger = logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var audit = await InitializeAudit(context);
            var result = await next();
            var finalResult = result.Result as ObjectResult;

            audit.ResponseStatusCode = context.HttpContext.Response.StatusCode;
            if (finalResult != null)
            {
                audit.Response = JsonConvert.SerializeObject(finalResult.Value);
            }
            logger.LogInformation(JsonConvert.SerializeObject(audit));
        }


    }
}
