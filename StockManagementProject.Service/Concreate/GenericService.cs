using StockManagementProject.Entities.Entities.Abstract;
using StockManagementProject.Repositories.Abstract;
using StockManagementProject.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Service.Concreate
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repo;

        public GenericService(IGenericRepository<T> repo)
        {
            this._repo = repo;
        }
        public bool Activate(int Id)
        {
            if (Id == 0 || GetById(Id) == null)
                return false;
            else
                _repo.Activate(Id);
            return true;
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _repo.Any(expression);
        }

        public bool Create(T entity)
        {
            return _repo.Create(entity);
        }

        public bool Create(List<T> entities)
        {
            return _repo.Create(entities);
        }

        public bool Delete(T entity)
        {
            return _repo.Delete(entity);    
        }

        public bool Delete(int Id)
        {
            return _repo.Delete(Id);
        }

        public bool DeleteAll(Expression<Func<T, bool>> expression)
        {
           return _repo.DeleteAll(expression);
        }

        public void DetachEntity(T entity)
        {
            _repo.DetachEntity(entity);
        }

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            return _repo.GetActive(includes);
        }

        public List<T> GetActive()
        {
            return _repo.GetActive();   
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return _repo.GetAll(includes);
        }

        public T GetByDefault(Expression<Func<T, bool>> expression)
        {
            return _repo.GetByDefault(expression);
        }

        public T GetById(int Id)
        {
            return _repo.GetById(Id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression)
        {
           return _repo.GetDefault(expression);
        }

        public int Save()
        {
            return _repo.Save();
        }

        public bool Update(T entity)
        {
            return _repo.Update(entity);
        }
    }
}
