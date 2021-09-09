using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.MiddleWares
{
	public class ResponseException
	{
		private readonly RequestDelegate _next;
        private readonly ILogger<ResponseException> _logger;
		public ResponseException(ILogger<ResponseException> logger, RequestDelegate next)
		{
			this._logger = logger;
			_next = next;
        }

		public async Task Invoke(HttpContext httpContext, IUtilityService utilityService)
		{
			try
			{
				await _next(httpContext);
			}
			catch (BusinessException ex)
			{
				await HandleExceptionAsync(httpContext, utilityService, ex, ex.HttpResponseStatusCode);
			}
			catch (ExternalServiceExceptionBase ex)
			{
				await HandleExceptionAsync(httpContext, utilityService, ex, StatusCodes.Status414RequestUriTooLong);
			}
			catch (ApiException ex)
			{
				await HandleExceptionAsync(httpContext, utilityService, ex, StatusCodes.Status500InternalServerError);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, utilityService, ex, StatusCodes.Status500InternalServerError);
			}
		}

		private Task HandleExceptionAsync(HttpContext httpContext, IUtilityService utilityService, Exception exception, int statusCode)
		{
			_logger.LogError(exception, "");
			httpContext.Response.ContentType = "application/json";
			return httpContext.Response.WriteAsync((new ApiResponse(utilityService.GetCurrentRefCodePerScope(), exception, statusCode != 0 ? statusCode : StatusCodes.Status500InternalServerError)).ToString());
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.

}
