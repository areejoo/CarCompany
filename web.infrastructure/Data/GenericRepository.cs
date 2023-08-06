using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.core.Interfaces;
using web.core.Entities;
using web.infrastructure.Data;
using System.Linq;
using System;

namespace web.infrastructure.Data
{

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MyAppDbContext context;
        private readonly DbSet<T> entities;
        string errorMessage = string.Empty;  
        public GenericRepository(MyAppDbContext context) {  
            this.context = context;  
            entities = context.Set < T > ();  
        }  

        public IQueryable<T>  GetQuerable(int pageIndex, int pageSize) {  
            return entities.AsQueryable().Skip((pageIndex - 1) * pageSize);

        }  
        
        public T GetById(Guid id) {  
            return entities.SingleOrDefault(s => s.Id == id);  
        }  
        
        public void Add(T entity) {  
            if (entity == null) {  
                throw new ArgumentNullException("entity");  
            }  
            entities.Add(entity);  
            context.SaveChanges();  
        }  
        
        public void Update(T entity) {  
            if (entity == null) {  
                throw new ArgumentNullException("entity");  
            }  
            context.SaveChanges();  
        }  
        public void Delete(Guid id) {  
            if (id == null) {  
                throw new ArgumentNullException("entity");  
            }  
            // T obj = context.entities.Where(a => a.Id == id).FirstOrDefault();
            // entities.Remove(obj);
            var entity= entities.Where(c => c.Id == id);
            context.Remove(entity);

            context.SaveChanges();  
        }  
    }  
}