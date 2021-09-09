using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace Mofid.eWallet.Infra.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundBusinessException : BusinessException
    {
        public NotFoundBusinessException(ExceptionErrorCodes errorCode, string additionalMessage = null) : base(errorCode, additionalMessage)
        {
            HttpResponseStatusCode = (int)HttpStatusCode.NotFound;
        }
    }

    [ExcludeFromCodeCoverage]
    public class InvalidDataBusinessException : BusinessException
    {
        public InvalidDataBusinessException(ExceptionErrorCodes errorCode, string additionalMessage = null) : base(errorCode, additionalMessage)
        {
            HttpResponseStatusCode = (int)HttpStatusCode.BadRequest;
        }
    }

    [ExcludeFromCodeCoverage]
    public class ExternalServiceCorruptionException : BusinessException
    {
        public ExternalServiceCorruptionException(ExceptionErrorCodes errorCode, string additionalMessage = null) : base(errorCode, additionalMessage)
        {
            HttpResponseStatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
