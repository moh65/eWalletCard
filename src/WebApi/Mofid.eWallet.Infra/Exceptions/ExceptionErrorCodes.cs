namespace Mofid.eWallet.Infra.Exceptions
{
	public enum ExceptionErrorCodes:int 
	{
		UnknownError = 100,
		NullTokenSemantic = 101,
		UserNotFound = 102,
		NotMatchedClients = 103,
		HandshakeFaileException = 104,
		TbsClientNotFound = 105,
		ClientNotFound = 106,
		LoginFailed= 107,
		OtpVerificationFailed = 108,
        OtpSendFailed = 109,
        BankRegistrationFailed = 110,
        BankTokenFailed = 111,
        RefreshTokenFailed = 112,
        BankProfileFailed = 113,
        BankEditProfileFailed = 114,
		BankPhysicalVerifyFailed = 115,
        InputParameterIsWrong = 116,
        CardNotRegistered = 117,
        TokenNotFound = 118,
		CardAlreadyActivated = 119,
	}
}