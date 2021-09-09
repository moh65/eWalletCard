using System.Diagnostics.CodeAnalysis;

namespace Mofid.eWallet.Infra.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class BusinessException : ExceptionBase
	{
		public int HttpResponseStatusCode { get; set; }
		public BusinessException(ExceptionErrorCodes errorCode, string additionalMessage=null) : base(errorCode, additionalMessage)
		{
		}

		public BusinessException(string message) : base(message)
		{
		}

		
	}
}
