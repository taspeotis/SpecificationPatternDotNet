using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    internal class ReadOnlySpecification<TEntity> : Specification<TEntity>
    {
        public ReadOnlySpecification(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate;
        }

        protected override Expression<Func<TEntity, bool>> Predicate { get; }
    }
}