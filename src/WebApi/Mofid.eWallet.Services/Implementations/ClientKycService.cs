using Microsoft.Extensions.Logging;
using Mofid.eWallet.Api.Dto;
using Mofid.eWallet.Application;
using Mofid.eWallet.Application.Registration.RegistrationStages;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.DTOs;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Caches;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;

using Mofid.eWallet.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Implementations
{
	public class ClientKycService : IClientKycService
	{
		private ILogger<ClientKycService> _logger;

		private readonly IClientRepository clientRepository;
		private readonly ICache cache;
		private readonly IBankRegistrationExternalServcie bankRegistrationExternal;
		private readonly IBankTokenExternalService bankTokenService;
		private readonly ICustomerBackOfficeService customerBOService;
		private readonly IClientStateService clientStateService;
		private readonly IBankCustomerRemainBalanceService bankCustomerRemainBalanceServiceService;
		private readonly ITemporaryClientRepository temporaryClientRepository;
		public ClientKycService(
			ITemporaryClientRepository temporaryClientRepository,
			IBankCustomerRemainBalanceService bankCustomerRemainBalanceServiceService,
			IClientRepository clientRepository,
			ICache cache,
			ITokenService acquireService,
			IBankRegistrationExternalServcie bankRegistrationExternal,
			IBankTokenExternalService bankTokenService,
			ICustomerBackOfficeService customerBOService,
			IClientStateService clientStateService, ILogger<ClientKycService> logger)
		{
			this.bankCustomerRemainBalanceServiceService = bankCustomerRemainBalanceServiceService;
			this.clientRepository = clientRepository;
			this.cache = cache;
			this.bankRegistrationExternal = bankRegistrationExternal;
			this.bankTokenService = bankTokenService;
			this.customerBOService = customerBOService;
			this.clientStateService = clientStateService;
			this.temporaryClientRepository = temporaryClientRepository;
			this._logger = logger;
		}

		public async Task<Client> GetAddress(Client data)
		{
			var getTBSinfo = await customerBOService.GetCustomerByNationalCodeAsync(data.NationalCode);

			return getTBSinfo;
		}

		public async Task<ClientRemainDto> GetTbsRemain(string nationalCode)
		{
			var remain = await customerBOService.GetCustomerRemain(nationalCode);

			return remain;
		}
		public async Task KycAsync(Client client, Token token)
		{
			_logger.Log(LogLevel.Debug, client.NationalCode);
			_logger.Log(LogLevel.Debug, token.DeviceId);
			var storedClient = await clientRepository.FindByNationalCodeAsync(client.NationalCode);

			if (storedClient is null)
				storedClient = await StoreNewClient(client, token);
			else
				storedClient = await UpdateClient(client, storedClient);

			_logger.Log(LogLevel.Debug, storedClient.Tokens.First().DeviceId);
			

			var worker = new Worker<Client>();
			worker.AddState(new HandshakeStage(clientRepository , bankRegistrationExternal, token.DeviceId) { Data = storedClient });
			worker.AddState(new OtpClientStage(clientRepository, bankRegistrationExternal, token.DeviceId) { Data = storedClient });

			await worker.ExecuteAsync();
		}


		public async Task Verify(Client clientInfo, Token tokenInfo, string otp)
		{
			var client = await clientRepository.FindByPhoneNumberAsync(clientInfo.PhoneNumber);

			if (client == null)
				throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound);

			var worker = new Worker<Client>();
			worker.AddState(new VerifyStage(clientRepository, bankRegistrationExternal, bankTokenService, cache, tokenInfo.DeviceId, otp) { Data = client });
			worker.AddState(new Level3Stage(clientRepository, bankRegistrationExternal, tokenInfo.DeviceId) { Data = client });
			worker.AddState(new Level4Stage(clientRepository, bankRegistrationExternal, tokenInfo.DeviceId) { Data = client });

			await worker.ExecuteAsync();
		}

		public async Task PhysicalVerifing(string nationalCode)
		{
			Client client = await clientRepository.FindByNationalCodeAsync(nationalCode);
			if (client is null)
				throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound);

			if (!await clientStateService.IsStatePassed(client.NationalCode, ClientState.PhysicalVerificated))
			{
				await bankRegistrationExternal.PhysicalVerifyAsync(client.UserId.ToString(), nationalCode);
				await clientStateService.PassStateAsync(client.NationalCode, ClientState.PhysicalVerificated);
			}
		}



		private async Task<Client> StoreNewClient(Client client, Token token)
		{
			var tbsClient = await customerBOService.GetCustomerByNationalCodeAsync(client.NationalCode);
			tbsClient.NationalCardSerial = client.NationalCardSerial;

			tbsClient.PhoneNumber =
				string.IsNullOrWhiteSpace(client.PhoneNumber)
				? tbsClient.PhoneNumber
				: client.PhoneNumber;

			Address item = client.Addresses.FirstOrDefault();
			if (item != null)
			{
				if (!tbsClient.Addresses.Any(x => x.AddressString == item.AddressString && x.Postalcode == item.Postalcode))
				{
					tbsClient.Addresses.Add(item);
					tbsClient.ChangeDefaultAddress(item);
				}
			}
			tbsClient.BirthDate = string.IsNullOrEmpty(client.BirthDate) ? tbsClient.BirthDate : client.BirthDate;

			await clientRepository.AddAsync(tbsClient);
			await clientStateService.PassStateAsync(tbsClient.NationalCode, ClientState.TbsVerified);
			return tbsClient;
		}


		private async Task<Client> UpdateClient(Client client, Client storedClient)
		{

			storedClient.BirthDate = string.IsNullOrWhiteSpace(client.BirthDate) ? storedClient.BirthDate : client.BirthDate;
			storedClient.PhoneNumber = string.IsNullOrWhiteSpace(client.PhoneNumber) ? storedClient.PhoneNumber : client.PhoneNumber;
			var item = client.Addresses.FirstOrDefault();
			if (item != null)
			{
				if (!storedClient.Addresses.Any(x => x.AddressString == item.AddressString && x.Postalcode == item.Postalcode))
				{
					storedClient.Addresses.Add(item);
					storedClient.ChangeDefaultAddress(item);
				}
			}

			await clientRepository.UpdateClientAsync(storedClient);

			return storedClient;
		}

		public async Task<ClientRemainDto> GetBankRemain(string nationalCode, string deviceId)
		{
			var findUser = await clientRepository.FindByNationalCodeAsync(nationalCode);
			if (findUser == null)
				throw new BusinessException(ExceptionErrorCodes.ClientNotFound);

			// todo if need user token get user token and pass it to bank

			var result = await bankCustomerRemainBalanceServiceService.GetRemain(nationalCode, deviceId);

			return new ClientRemainDto
			{
				CurrentRemain = result,
			};
		}

		public async Task<bool> IsLegal(string nationalCode)
		{
			return await temporaryClientRepository.IsInWhiteList(nationalCode);
		}

		public async Task<List<ClientState>> GetClientStates(string nationalCode)
		{
			return await clientStateService.GetStates(nationalCode);

		}

		public async Task<(long, List<ClientDto>)> ListAsync(int skip, int take, string nationalCode, string mobile)
		{
			var clients = await clientRepository.TakeAsync(skip, take, nationalCode, mobile);
			return (clients.Item1, clients.Item2.Select(s => s.MapToDto()).ToList());
		}

		public async Task<ClientDto> GetClientAsync(string nationalCode)
		{
			var client = await clientRepository.FindByNationalCodeAsync(nationalCode);
			if (client is null)
				throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound, "مشتری یافت نشد ");

			return client.MapToDto();
		}
	}
}
