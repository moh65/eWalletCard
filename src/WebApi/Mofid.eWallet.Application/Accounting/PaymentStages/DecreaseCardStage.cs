using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application.Accounting.PaymentStages
{
	public class DecreaseCardStage : StageBase<Transaction>
	{
		private readonly ITransactionRepository transactionRepository;
		private readonly IBankCustomerRemainBalanceService bankCustomerRemainBalanceServiceService;

		public DecreaseCardStage(
			ITransactionRepository transactionRepository,
			IBankCustomerRemainBalanceService bankCustomerRemainBalanceServiceService,
			string nationalCode, decimal amount, string groupId, string deviceId)
		{
			this.transactionRepository = transactionRepository;
			this.bankCustomerRemainBalanceServiceService = bankCustomerRemainBalanceServiceService;
			Data = new Transaction
			{
				Amount = amount,
				NationalCode = nationalCode,
				TransactionStatus = TransactionStatus.NotStarted,
				Type = TransactionType.DecreaseBank,
				GroupId = groupId,
				DeviceId = deviceId
			};
		}

		public async override Task<bool> Do()
		{
			await transactionRepository.AddAsync(Data);
			var remain = await bankCustomerRemainBalanceServiceService.GetRemain(Data.NationalCode, Data.DeviceId);
			if (Data.Amount > remain)
			{
				Data.TransactionStatus = TransactionStatus.UnSuccessDontNeedReverse;
				Data.Description += "unsuccess - Not enuphe mony in card";
				await transactionRepository.UpdateAsync(Data);
				return false;
			}
			else
			{
				try
				{
					//TODO: call bank service
					Data.TransactionStatus = TransactionStatus.Success;
					Data.ModifiedDate = DateTime.Now;
					await transactionRepository.UpdateAsync(Data);
					return true;
				}
				catch (Exception)
				{
					//TODO: call bank reverse service 
					Data.TransactionStatus = TransactionStatus.UnSuccessDontNeedReverse;
					Data.Description += "unsuccess - error ocured in do transaction";
					Data.ModifiedDate = DateTime.Now;
					await transactionRepository.UpdateAsync(Data);
					return false;
				}
			}
		}

		public async override Task<bool> Undo()
		{
			if (Data.TransactionStatus != TransactionStatus.Success)
				return true;

			Data.TransactionStatus = TransactionStatus.Undo_Notstarted;
			await transactionRepository.AddAsync(Data);

			try
			{
				//TODO: call bank undo service
				Data.TransactionStatus = TransactionStatus.Undo_Success;
				Data.ModifiedDate = DateTime.Now;
				await transactionRepository.UpdateAsync(Data);
				return true;
			}
			catch (Exception)
			{
				Data.TransactionStatus = TransactionStatus.Undo_UnSuccess;
				Data.ModifiedDate = DateTime.Now;
				await transactionRepository.UpdateAsync(Data);

				return false;
			}

		}
	}
}
