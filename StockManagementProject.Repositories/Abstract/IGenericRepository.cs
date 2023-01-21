using StockManagementProject.Entities.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        bool Create(T entity);

        bool Create(List<T> entities);

        bool Update(T entity);

        bool Delete(T entity);

        bool Delete(int Id);

        bool DeleteAll(Expression<Func<T, bool>> expression);

        T GetById(int Id);

        List<T> GetDefault(Expression<Func<T, bool>> expression);
        T GetByDefault(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes);
        List<T> GetActive();
        bool Activate(int Id);

        bool Any(Expression<Func<T, bool>> expression);

        int Save();

        void DetachEntity(T entity);




    }
}
