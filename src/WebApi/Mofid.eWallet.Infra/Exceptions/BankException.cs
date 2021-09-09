using System.ComponentModel;
using System.Linq;


namespace Mofid.eWallet.Infra.Exceptions
{

    public class BankException : BusinessException
    {
        public BankException(ExceptionErrorCodes errorCode, int code, string additionalMessage = null) :
            base(errorCode, string.IsNullOrEmpty(additionalMessage) ? ClientMessage(code) : additionalMessage)
        {
            HttpResponseStatusCode = (int)errorCode;
        }

        public BankException(ExceptionErrorCodes errorCode, BankErrorCode code, string additionalMessage = null) :
           base(errorCode, string.IsNullOrEmpty(additionalMessage) ? ClientMessage((int)code) : additionalMessage)
        {
            HttpResponseStatusCode = (int)errorCode;
        }

        public BankException(int code, string additionalMessage = null) : base(string.IsNullOrEmpty(additionalMessage) ? ClientMessage(code) : additionalMessage)
        {
            HttpResponseStatusCode = (int)ExceptionErrorCodes.UnknownError;
        }


        public static string ClientMessage(int code)
        {
            try
            {
                var enumType = typeof(BankErrorCode);
                var memberInfos = enumType.GetMember(((BankErrorCode)code).ToString());
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
                var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)valueAttributes[0]).Description;
                return description;

            }
            catch (System.Exception)
            {
                return "";
            }
        }
    }

    public enum BankErrorCode : int
    {
        [Description("خطا")]
        Failed = 0,

        [Description("کاربر احراز هویت نشده")]
        UserNotAuthenticated = 21,
    }
}
