using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> predicate = null);
		/// <summary>
		/// throw exception if cannot found
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		Task<T> FindAsync(Expression<Func<T, bool>> predicate = null);
	}
}
