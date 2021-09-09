using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Infra.Responses
{
    public class ApiResponse
    {
        public ApiResponse(string refCode, string message)
        {
            StatusCode = StatusCodes.Status200OK;
            Message = message;
            IsSuccess = true;
            ReferenceCode = refCode;
        }

        public ApiResponse(string refCode, int statusCode, string message = "")
        {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = true;
            ReferenceCode = refCode;
        }

        public ApiResponse(string refCode, Exception ex, int statusCode)
        {
            StatusCode = statusCode;
            if (statusCode != StatusCodes.Status500InternalServerError)
                Message = ex.Message;
            IsSuccess = false;
            ReferenceCode = refCode;
        }



        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ReferenceCode { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse(string refCode, T _result) : base(refCode,"")
        {
            Result = _result;
        }
        public T Result { get; set; }

    }
}
