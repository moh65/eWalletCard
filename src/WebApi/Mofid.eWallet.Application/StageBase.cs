using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application
{
	public abstract class StageBase<T>
		where T : class
	{
		public T Data { get; set; }
		public abstract Task<bool> Do();
		public abstract Task<bool> Undo();
	}
}
