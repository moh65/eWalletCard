using Mofid.eWallet.Infra.Exceptions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.ElasticSearch
{
    public class ElasticLogger<T> where T : class
    {
        private ElasticClientProvider provider;
        private bool IsExist { get { return logDate.Date == DateTime.Now.Date; } }
        private static DateTime logDate;

        private string IndexName = $"{typeof(T).Name.ToLower()}-*";
        public ElasticLogger(ElasticClientProvider provider)
        {
            this.provider = provider;
        }

        private async Task CheckDatabase()
        {
            if (IsExist) return;
            var exist = await provider.Client.Indices.ExistsAsync(IndexName);
            if (exist.Exists)
            {
                logDate = DateTime.Now.Date;
                return;
            }
            await provider.Client.Indices.CreateAsync(IndexName);

        }

        public async Task Save(T obj)
        {
            await CheckDatabase();
            await provider.Client.IndexAsync(obj, i => i.Index(IndexName));
        }

       

        public async Task<( long ,List<T>)> TaskAsync(int skip , int take ,string nationalCode)
        {
            if (string.IsNullOrEmpty(nationalCode))
                throw new BusinessException(ExceptionErrorCodes.InputParameterIsWrong, "کد ملی را وارد کنید");

            Field field = new Field("msg");
            var result = await provider.Client.SearchAsync<T>(s =>
               s.Query(q => q.Match(z => z.Field("userCode").Query(nationalCode))).Skip(skip).Take(take));
            long total = result.Total;
            var docs = result.Documents.Cast<T>();
            return (total, docs.ToList());
        }


    }

}

