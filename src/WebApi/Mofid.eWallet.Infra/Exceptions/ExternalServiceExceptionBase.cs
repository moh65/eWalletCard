using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Mofid.eWallet.Infra.Exceptions
{
    [ExcludeFromCodeCoverage]
	public class ExternalServiceExceptionBase : ExceptionBase
	{
		public ExternalServiceExceptionBase(ExceptionErrorCodes errorCode, string additionalMessage = null) : base(errorCode, additionalMessage)
		{
		}
		public ExternalServiceExceptionBase(string message) : base(message)
		{
		}
	}
}
