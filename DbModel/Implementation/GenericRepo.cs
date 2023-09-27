using DbModel.Context;
using DbModel.Interface;
using DbModel.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly BasketContext _basketContext;
        public GenericRepo(BasketContext basketContext)
        {
            _basketContext= basketContext;
        }
        public void Add(T entity)
        {
           _basketContext.Set<T>().Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Delete(T entity)
        {
            _basketContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllListAsync()
        {
            return await _basketContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _basketContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_basketContext.Set<T>().AsQueryable(), spec);
        }
        public void Update(T entity)
        {
            _basketContext.Set<T>().Attach(entity);
            _basketContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
