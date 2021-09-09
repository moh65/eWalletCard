using Mofid.eWallet.Infra.ElasticSearch;
using System;
using System.Diagnostics;

namespace Mofid.eWallet.Infra.Utils
{
    public interface IUtilityService
    {
        DateTime GetNow();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>yyyy/mm/dd</returns>
        string ConvertToPersian(DateTime date);
        string GetCurrentRefCodePerScope();

        string HashPassword(string password);

        Audit PodAudit<T>(T response ,string Request  , string nationalCode, int StatusCode = 200 , double time = 0);

        Stopwatch StartNewWatch();

        double TotalTime(Stopwatch stopwatch);
       
    }
}