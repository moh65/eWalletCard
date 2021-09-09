using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Mofid.eWallet.Infra.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Audit = Mofid.eWallet.Infra.ElasticSearch.Audit;

namespace Mofid.eWallet.Api.Filters
{
    public abstract class AuditFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly IUtilityService utilityService;

        public AuditFilterAttribute(IUtilityService utilityService)
        {
            this.utilityService = utilityService;
        }
        public abstract Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);
        protected async Task<string> FormatRequest(HttpContext context)
        {
            //using (var reader = new StreamReader(context.Request.Body))
            //{
            //    reader.AllowSynchronousIO = true;
            //    var body = await reader.ReadToEndAsync();
            //    var body2 = reader.ReadToEnd();

            //    // Do something
            //}

            HttpRequest request = context.Request;
            request.EnableBuffering();
            //request.EnableRewind();

            //var stream = new StreamReader(request.Body);
            //var body = stream.ReadToEnd();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            string password = "password";
            if (bodyAsText.ToLower().Contains(password))
            {
                var obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(bodyAsText.ToLower());
                obj.Property(password).Remove();
                bodyAsText = obj.ToString();
            }
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
        protected async Task<Audit> InitializeAudit(ActionExecutingContext context)
        {
            var requestData = await FormatRequest(context.HttpContext);
            string ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            var audit = new Audit
            {
                DateTime = DateTime.Now,
                Username = "",
                Ip = ip,
                Request = requestData,
                ActionType = context.HttpContext.Request.Method,
                ReferenceCode = utilityService.GetCurrentRefCodePerScope(),
            };
            return audit;
        }
    }

}
