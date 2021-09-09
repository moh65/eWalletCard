using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application.Accounting.PaymentStages
{
	public class IncreaseCardStage : StageBase<Transaction>
	{
		private readonly ITransactionRepository transactionRepository;

		public IncreaseCardStage(
			ITransactionRepository transactionRepository,
			string nationalCode, decimal amount, string groupId, string deviceId)
		{
			this.transactionRepository = transactionRepository;
			Data = new Transaction
			{
				Amount = amount,
				NationalCode = nationalCode,
				TransactionStatus = TransactionStatus.NotStarted,
				Type = TransactionType.IncreaseBank,
				GroupId = groupId,
				DeviceId = deviceId
			};
		}

		public async override Task<bool> Do()
		{
			await transactionRepository.AddAsync(Data);

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
