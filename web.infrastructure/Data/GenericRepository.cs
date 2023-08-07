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

        public   IQueryable<T>GetQueryable() {
            return    entities.AsQueryable();

        }  
        
        public async Task<T> GetByIdAsync(Guid id) {  
            return  await entities.FirstOrDefaultAsync(s => s.Id == id);  
        }  
        
        public async Task Add(T entity) {  
            if (entity == null) {  
                throw new ArgumentNullException("entity");  
            }  
             await entities.AddAsync(entity);  
            context.SaveChanges();  
        }  
        
        public async Task Update(T entity) {
            var car = await entities.FirstOrDefaultAsync(s => s.Id == entity.Id);
            if (car == null) {  
                throw new EntryPointNotFoundException("entity");  
            }  
             await context.SaveChangesAsync();  
        }  
        public async Task Delete(Guid id) {  
           ;
            var entity=  await entities.FirstOrDefaultAsync(s => s.Id == id);
            if (entity != null)
            {
                context.Remove(entity);

                await context.SaveChangesAsync();
            }
            else {
                throw new EntryPointNotFoundException("entity");
            }
        }  
    }  
}