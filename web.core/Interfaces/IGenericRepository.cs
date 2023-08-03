using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.core.Entities;

namespace web.core.Interfaces
{
public interface IGenericRepository<T> where T : BaseEntity
{
  IQueryable<T> GetAll();  
        T GetById(Guid id);  
        void Add(T entity);  
        void Update(T entity);  
        void Delete(Guid id);
}
}