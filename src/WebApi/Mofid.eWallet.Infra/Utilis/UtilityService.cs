using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mofid.eWallet.Infra.ElasticSearch;
using Mofid.eWallet.Infra.Security;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mofid.eWallet.Infra.Utils
{
	public class UtilityService : IUtilityService
	{
		private string _refCode;
		
		private readonly ILogger<UtilityService> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UtilityService(ILogger<UtilityService> logger , IHttpContextAccessor HttpContextAccessor)
        {
		
			this.logger = logger;
            httpContextAccessor = HttpContextAccessor;
            _refCode = Guid.NewGuid().ToString("N");
			MappedDiagnosticsLogicalContext.SetScoped("RequestRefCode", _refCode);
        }
		public string ConvertToPersian(DateTime date)
		{
			var pc = new PersianCalendar();
			return string.Format("{0}/{1}/{2}",
				pc.GetYear(date).ToString("0000"),
				pc.GetMonth(date).ToString("00"),
				pc.GetDayOfMonth(date).ToString("00"));
		}
		public string GetCurrentRefCodePerScope() => _refCode;
		
        public DateTime GetNow()
		{
			return DateTime.Now;
		}
		private void SetUserLog(string nationalCode)
        {
			MappedDiagnosticsLogicalContext.Set("userCode", nationalCode);
		}
        public string HashPassword(string password)
        {
			SHA256 sha256 = SHA256.Create();
			byte[] hashValue;
			UTF8Encoding objUtf8 = new UTF8Encoding();
			hashValue = sha256.ComputeHash(objUtf8.GetBytes(password));
			return Convert.ToBase64String(hashValue);
		}

		public Audit PodAudit<T>(T response ,string request , string nationalCode, int StatusCode = 200 , double time = 0)
        {
			SetUserLog(nationalCode);
			var audit =new Audit
			{
				ActionType = "POD",
				DateTime = GetNow(),
				ReferenceCode = _refCode,
				Response = JsonConvert.SerializeObject(response) , 
				ResponseStatusCode = StatusCode,
				Ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
				ResponseTime = time,
				Request = request,
				Username = nationalCode,
			};


			logger.LogInformation(JsonConvert.SerializeObject(audit));
			return audit;
		}

        public Stopwatch StartNewWatch()
        {
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

        public double TotalTime(Stopwatch stopwatch)
        {
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		} 
    }

}
