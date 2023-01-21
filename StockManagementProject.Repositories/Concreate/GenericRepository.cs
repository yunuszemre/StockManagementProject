using StockManagementProject.Entities.Entities.Abstract;
using StockManagementProject.Repositories.Abstract;
using StockManagementProject.Repositories.Context;
using StockManagementProject.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace StockManagementProject.Repositories.Concreate
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StockManagementProjectContext _context;

        public GenericRepository(StockManagementProjectContext context)
        {
            this._context = context;
        }
        public bool Activate(int Id)
        {
            T item = GetById(Id);
            item.Status = Status.Active;
            return Update(item);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public bool Create(T entity)
        {
            try
            {
                
                
                _context.Add(entity);
                return Save() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Create(List<T> entities)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (T item in entities)
                    {
                        item.CreateDate = DateTime.Now;
                        _context.Set<T>().Add(item);
                    }
                    //_context.Set<T>().AddRange(items);
                    ts.Complete(); // Tüm işlemler başarılı olduğunda, yani tüm ekleme işlemleri başarılı olduğunda Complete() olmuş olacak.
                    return Save() > 0; // 1 veya daha fazla satır ekleniyorsa...
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            entity.Status = Status.Passive; return Update(entity);
        }

        public bool Delete(int Id)
        {
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    T item = GetById(Id);
                    item.Status = Status.Passive;

                    return Update(item);

                }
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool DeleteAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(expression);

                    int counter = 0;
                    foreach (var item in collection)
                    {
                        item.Status = Status.Passive;
                        bool res = Update(item);
                        if (res) counter++;

                    }
                    if (collection.Count == counter) ts.Complete();
                    else return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DetachEntity(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Detached;
        }

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x => x.Status == Status.Active);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public List<T> GetActive()
        {
            return _context.Set<T>().Where(x => x.Status == Status.Active).ToList();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public T GetById(int Id)
        {
            return _context.Set<T>().Find(Id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                entity.ModifiedDate = DateTime.Now;
                _context.Set<T>().Update(entity);
                return Save() > 0;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public void Detached(T item)
        {
            _context.Entry<T>(item).State = EntityState.Detached;   // Bir entry'i takip etmeyi bırakmak için kullanılır.
        }

        public T GetByDefault(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);
        }
    }
}
