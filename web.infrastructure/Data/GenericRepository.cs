using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.core.Interfaces;
using web.core.Entities;
using web.infrastructure.Data;
using System.Linq;
using System;
using System.Data;


namespace web.infrastructure.Data
{

    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MyAppDbContext context;
        private readonly DbSet<T> entities;
        string errorMessage = string.Empty;
        public GenericRepository(MyAppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetQueryable()
        {
           

            return entities.AsQueryable();

        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await entities.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var c=await entities.AddAsync(entity);
            var n=context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            var entityExists = await entities.AsNoTracking().FirstOrDefaultAsync(s => s.Id == entity.Id);
            
            if (entityExists == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException();
            }
            var c = context.Update(entity); 
            //context.Entry(entityExists).CurrentValues.SetValues(entity);
            var w=await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {

            var entity = await entities.FirstOrDefaultAsync(s => s.Id == id);
            if (entity != null)
            {
                context.Remove(entity);

                await context.SaveChangesAsync();
            }
            else
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException();
            }
        }
    }
}