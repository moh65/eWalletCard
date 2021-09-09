using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application
{
	public class Worker<T>
		where T : class
	{
		private LinkedList<StageBase<T>> Stages { get; set; } = new LinkedList<StageBase<T>>();
		public async Task<bool> ExecuteAsync()
		{
			var errorOcured = false;
			foreach (var item in Stages)
			{
				if (!await item.Do())
				{
					errorOcured = true;
					break;
				}
			}
			if (errorOcured)
			{
				foreach (var item in Stages)
				{
					await item.Undo();
				}
			}

			return !errorOcured;
		}

		public void AddState(StageBase<T> Stage)
		{
			Stages.AddLast(Stage);
		}
	}
}
