using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    internal class ReadOnlySpecification<TEntity> : Specification<TEntity>
    {
        private readonly Expression<Func<TEntity, bool>> _predicate;

        public ReadOnlySpecification(Expression<Func<TEntity, bool>> predicate)
        {
            _predicate = predicate;
        }

        protected override Expression<Func<TEntity, bool>> Predicate
        {
            get { return _predicate; }
        }
    }
}