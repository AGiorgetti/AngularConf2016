using WebApplication.Monitoring.Core;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication.Monitoring.Repository
{

    public abstract class InMemoryRepository<T, TKey> : ICollectionRepository<T, TKey>
            where T : IRepositoryObject<TKey>, new()
    {
        protected List<T> Collection { get; set; } = new List<T>();

        public T LoadById(TKey id)
        {
            return Collection.AsQueryable<T>().SingleOrDefault(s => id.Equals(s.Id));
        }

        public void SaveOrUpdate(T obj)
        {
            if (obj == null)
            {
                return;
            }
            var itm = LoadById(obj.Id);
            if (itm == null)
            {
                Collection.Add(obj);
            }
            else
            {
                var idx = Collection.IndexOf(itm);
                Collection[idx] = obj;
            }
        }

        public void DeleteById(TKey id)
        {
            var itm = LoadById(id);
            if (itm != null)
            {
                Collection.Remove(itm);
            }
        }

        public IQueryable<T> Queryable()
        {
            return Collection.AsQueryable<T>();
        }
    }

}