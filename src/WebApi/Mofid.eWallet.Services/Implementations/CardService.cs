using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Implementations
{
	public class CardService : ICardService
    {
        private readonly IClientRepository clientRepository;
        private readonly IUtilityService utilityServcie;
        private readonly IBankCardService bankCardService;
        private readonly IClientStateService clientStateService;

        public CardService(
            IClientRepository clientRepository, 
            IUtilityService utilityServcie, 
            IBankCardService bankCardService, 
            IClientStateService clientStateService)
        {
            this.clientRepository = clientRepository;
            this.utilityServcie = utilityServcie;
            this.bankCardService = bankCardService;
            this.clientStateService = clientStateService;
        }

        public async Task ActivateAsync(string nationalCode, string phoneNumber, string pan)
        {
            if (string.IsNullOrWhiteSpace(nationalCode) && string.IsNullOrWhiteSpace(phoneNumber))
                throw new InvalidDataBusinessException(ExceptionErrorCodes.InputParameterIsWrong);

            var client = await clientRepository.FindByPhoneNumberAsync(phoneNumber);

            if (client is null)
                throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound);

            //TODO call bank api for activation on card
            await bankCardService.CardActivateAsync(client.Tokens.FirstOrDefault().AccessToken, client.UserId, pan, client.NationalCode);
            //var responseOfBank 
            client.MofidCard = new Card { CardNumber = pan, ActivateDate = utilityServcie.GetNow() };
            client.CardStatus = CardStatusEnum.Delivered;
            await clientRepository.UpdateClientAsync(client);
        }

        public async Task RegisterCardAsync(string nationalCode, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalCode) && string.IsNullOrWhiteSpace(phoneNumber))
                throw new InvalidDataBusinessException(ExceptionErrorCodes.InputParameterIsWrong);

            var client = await clientRepository.FindByPhoneNumberAsync(phoneNumber);

            if (client is null)
                throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound);

            //if (await clientStateService.IsStatePassed(client.NationalCode, ClientState.CardIssued))
            //    return;

            if (!client.Tokens.Any())
                throw new NotFoundBusinessException(ExceptionErrorCodes.TokenNotFound);
            
            await bankCardService.CardIssuanceAsync(client.Tokens.FirstOrDefault().AccessToken , client.NationalCode);
            await clientStateService.PassStateAsync(client.NationalCode, ClientState.CardIssued);
        }

        public async Task VerifyActivateAsync(string nationalCode, string phoneNumber, string otp)
        {
            if (string.IsNullOrWhiteSpace(nationalCode) && string.IsNullOrWhiteSpace(phoneNumber))
                throw new InvalidDataBusinessException(ExceptionErrorCodes.InputParameterIsWrong);

            var client = await clientRepository.FindByPhoneNumberAsync(phoneNumber);

            if (client is null)
                throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound);

            if (client.CardStatus == CardStatusEnum.Activated)
                throw new BusinessException(ExceptionErrorCodes.CardAlreadyActivated, "کارت قبلا فعال شده");

            if (client.CardStatus != CardStatusEnum.Delivered)
                throw new BusinessException(ExceptionErrorCodes.CardNotRegistered, "کارت فعال نشده");

          

            //TODO call bank api for activation on card
            await bankCardService.VerifyCardActivateAsync(client.Tokens.FirstOrDefault().AccessToken , client.PhoneNumber , otp, client.NationalCode);
            //var responseOfBank 
            //  client.MofidCard = new Card { CardNumber = pan, ActivateDate = utilityServcie.GetNow() };
            client.CardStatus = CardStatusEnum.Activated;
            await clientRepository.UpdateClientAsync(client);
        }

      
    }
}
