using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.core.Interfaces;
using web.core.Entities;

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
        public IEnumerable<T>  GetAll() {  
            return entities.AsEnumerable();  
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
        public void Delete(T entity) {  
            if (entity == null) {  
                throw new ArgumentNullException("entity");  
            }  
            entities.Remove(entity);  
            context.SaveChanges();  
        }  
    }  
}