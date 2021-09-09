using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Entities.Enum
{
	public enum TransactionStatus : int
	{
		NotStarted = 0,
		Success = 1,
		UnSuccessDontNeedReverse = 2,
		Undo_Notstarted = 3,
		Undo_Success = 4,
		Undo_UnSuccess = 5,
	}
}
