using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbModel.Specification
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification()
        {
            
        }
        public Specification(Expression<Func<T,bool>> _criteria)
        {
            Criteria = _criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDescending {get;set; }

        public int Take {get;set; }

        public int Skip {get;set; }

        public bool IsPagingEnabled {get;set; }
        public void AddInclude(Expression<Func<T,object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public void AddOrderBy(Expression<Func<T, object>> orderExpression)
        {
            OrderBy=orderExpression;
        }
        public void AddOrderByDescending(Expression<Func<T,object>> orderByExpression)
        {
            OrderByDescending=orderByExpression;
        }
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
