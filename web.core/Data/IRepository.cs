using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core;
using web.core.Data;
using web.core.Models;


namespace web.core.Data
{
    
public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task <T>Get(Guid id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid id);
    }

    
}
