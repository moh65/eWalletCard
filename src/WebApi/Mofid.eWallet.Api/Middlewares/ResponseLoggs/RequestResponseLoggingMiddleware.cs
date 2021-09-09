using Microsoft.AspNetCore.Http;
using Mofid.eWallet.Infra.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ElasticLogger<Audit> _auditer;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ElasticLogger<Audit> auditer)
        {
            _next = next;
            _auditer = auditer;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                string username = string.Empty;
                //if (!string.IsNullOrEmpty(username))
                //    username = context.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                string ip = context.Connection.RemoteIpAddress.ToString();
                var audit = new Audit { DateTime = DateTime.Now, Request = request, ActionType = context.Request.Path, Username = username, Ip = ip };
                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
                stopwatch.Stop();
                audit.ResponseTime = stopwatch.Elapsed.TotalSeconds;
                audit.Response = response;
                await _auditer.Save(audit);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}
