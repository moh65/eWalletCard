using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application.Accounting.PaymentStages
{
	public class DecreaseTbsStage : StageBase<Transaction>
	{
		private readonly ITransactionRepository transactionRepository;
        private readonly ICustomerBackOfficeService customerBackOfficeService;

        public DecreaseTbsStage(
			ITransactionRepository transactionRepository,
			ICustomerBackOfficeService CustomerBackOfficeService,
			string nationalCode, decimal amount, string groupId, string deviceId)
		{
			this.transactionRepository = transactionRepository;
            customerBackOfficeService = CustomerBackOfficeService;
            Data = new Transaction
			{
				Amount = amount,
				NationalCode = nationalCode,
				TransactionStatus = TransactionStatus.NotStarted,
				Type = TransactionType.DecreaseTBS,
				GroupId = groupId,
				DeviceId = deviceId
			};
            this.transactionRepository = transactionRepository;
        }

		public async override Task<bool> Do()
		{
			await transactionRepository.AddAsync(base.Data);
			var customer = await customerBackOfficeService.GetCustomerRemain(Data.NationalCode);
			if (customer.CurrentRemain < Data.Amount)
			{
				Data.TransactionStatus = TransactionStatus.UnSuccessDontNeedReverse;
				Data.Description += "unsuccess - Not enuphe mony in tbs";
				await transactionRepository.UpdateAsync(Data);
				return false;
			}

			try
            {	
				bool result = await customerBackOfficeService.DecreaseTbsBalanceAsync(Data.Amount, Data.NationalCode);

				if (result)
					Data.TransactionStatus = TransactionStatus.Success;
				else
					Data.TransactionStatus = TransactionStatus.UnSuccessDontNeedReverse;

				await transactionRepository.UpdateAsync(Data);
				return result;
			}
            catch (System.Exception)
            {
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
			Data.Id = null;

			try
            {
				await transactionRepository.AddAsync(Data);

				//#tbstoDecrease(amount, nationalCode);
				bool result = await customerBackOfficeService.IncreaseTbsBalanceAsync(Data.Amount, Data.NationalCode);

				if (result)
					Data.TransactionStatus = TransactionStatus.Undo_Success;
				else
					Data.TransactionStatus = TransactionStatus.Undo_UnSuccess;
				

				await transactionRepository.UpdateAsync(Data);

				return result;
			}
            catch (System.Exception)
            {
				Data.TransactionStatus = TransactionStatus.Undo_UnSuccess;
				Data.ModifiedDate = DateTime.Now;
				await transactionRepository.UpdateAsync(Data);
				return false;
            }
			
		}
	}
}
