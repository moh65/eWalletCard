namespace Mofid.eWallet.Entities.Enum
{
    public enum ClientState : int
    {
        TbsVerified = 0,
        HandshakedWithBank = 1,
        RegiteredNotVerified = 2,
        OtpVerified = 3,
        UpgradeToLevel3 = 4,
        UpgradeToLevel4 = 5,
        PhysicalVerificated = 6,
        CardIssuingPayment = 7,
        CardIssued = 8,
        CardActivated = 9,
    }
}
