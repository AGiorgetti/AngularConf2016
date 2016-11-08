using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Monitoring.Core
{
	public interface ICollectionRepository<T, TKey>
		where T : IRepositoryObject<TKey>, new()
	{
		T LoadById(TKey id);

		void SaveOrUpdate(T obj);

		void DeleteById(TKey id);

		IQueryable<T> Queryable();
	}

	public interface IRepositoryObject<TKey>
	{
		TKey Id { get; set; }
	}
}
