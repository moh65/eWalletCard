using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Mofid.eWallet.Infra.Exceptions
{
    [ExcludeFromCodeCoverage]
	public abstract class ExceptionBase : Exception
	{
		public ExceptionErrorCodes ErrorCode { get; set; }
		public ExceptionBase(ExceptionErrorCodes errorCode, string additionalMessage = null)
			: base(errorCode + " " + additionalMessage)
		{
			ErrorCode = errorCode;
		}
		public ExceptionBase(string message)
			: base(ExceptionErrorCodes.UnknownError + " " + message)
		{
			ErrorCode = ExceptionErrorCodes.UnknownError;
		}

		public override string ToString()
		{
			if (InnerException == null)
			{
				return base.ToString();
			}
			return string.Format(CultureInfo.InvariantCulture, "{0} [See nested exception: {1}]", base.ToString(), InnerException);
		}
	}
}
