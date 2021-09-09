using Microsoft.AspNetCore.Mvc.Filters;
using Mofid.eWallet.Infra.ElasticSearch;
using Mofid.eWallet.Infra.Utils;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Filters
{
    public class AuditRequest : AuditFilterAttribute
    {
        private ElasticLogger<Audit> _auditer;
        public AuditRequest(IUtilityService utilityService, ElasticLogger<Audit> auditer)
            : base(utilityService)
        {
            _auditer = auditer;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var audit = await InitializeAudit(context);
            await next();
            await _auditer.Save(audit);
        }
    }
}
