using Mofid.eWallet.Application;
using Mofid.eWallet.Application.Accounting;
using Mofid.eWallet.Application.Accounting.PaymentStages;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Implementations
{
	public class PaymentService : IPaymentService
	{
		private readonly ICustomerBackOfficeService customerBackOfficeServiceService;
        private readonly ITransactionRepository transactionRepositoryService;
        private readonly IClientRepository clientRepositoryService;
		private readonly IBankCustomerRemainBalanceService bankCustomerRemainBalanceServiceService;

        public PaymentService(
			ICustomerBackOfficeService customerBackOfficeServiceService,
			ITransactionRepository TransactionRepositoryService,
			IClientRepository clientRepositoryService,
			IBankCustomerRemainBalanceService bankCustomerRemainBalanceServiceService)
		{
			this.customerBackOfficeServiceService = customerBackOfficeServiceService;
            transactionRepositoryService = TransactionRepositoryService;
            this.clientRepositoryService = clientRepositoryService;
			this.bankCustomerRemainBalanceServiceService = bankCustomerRemainBalanceServiceService;
		}

		public async Task TbsToCard(string nationalCode, decimal amount)
		{
			var client = await clientRepositoryService.FindByNationalCodeAsync(nationalCode);
			if (client == null)
				throw new BusinessException(ExceptionErrorCodes.ClientNotFound, "کد ملی اشتباه است");

			var groupId = Guid.NewGuid().ToString("N");

			var worker = new Worker<Transaction>();

			worker.AddState(new DecreaseTbsStage(transactionRepositoryService,
				customerBackOfficeServiceService,
				nationalCode, amount, groupId, "deviceId"));
			worker.AddState(new IncreaseCardStage(transactionRepositoryService, 
				nationalCode, amount, groupId, "deviceId"));

			await worker.ExecuteAsync();
		}

		public async Task CardToTbs(string nationalCode, decimal amount)
		{
			var client = await clientRepositoryService.FindByNationalCodeAsync(nationalCode);
			if (client == null)
				throw new BusinessException(ExceptionErrorCodes.ClientNotFound, "کد ملی اشتباه است");

			var groupId = Guid.NewGuid().ToString("N");

			var worker = new Worker<Transaction>();

			worker.AddState(new DecreaseCardStage(transactionRepositoryService, bankCustomerRemainBalanceServiceService,
				nationalCode, amount, groupId, "deviceId"));
			worker.AddState(new IncreaseTbsStage(transactionRepositoryService,
				customerBackOfficeServiceService,
				nationalCode, amount, groupId, "deviceId"));

			await worker.ExecuteAsync();
		}
	}
}
