using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        protected abstract Expression<Func<TEntity, bool>> Predicate { get; }

        public IQueryable<TEntity> SatisfiedBy(IQueryable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            if (Predicate == null) throw new InvalidOperationException();

            return entities.Where(Predicate);
        }

        public Specification<TDerivedEntity> AndAlso<TDerivedEntity>(Specification<TDerivedEntity> otherSpecification)
            where TDerivedEntity : TEntity
        {
            if (otherSpecification == null) throw new ArgumentNullException("otherSpecification");
            if (Predicate == null) throw new InvalidOperationException();

            return new ReadOnlySpecification<TDerivedEntity>(Predicate.AndAlso(otherSpecification.Predicate));
        }

        public Specification<TDerivedEntity> OrElse<TDerivedEntity>(Specification<TDerivedEntity> otherSpecification)
            where TDerivedEntity : TEntity
        {
            if (otherSpecification == null) throw new ArgumentNullException("otherSpecification");
            if (Predicate == null) throw new InvalidOperationException();

            return new ReadOnlySpecification<TDerivedEntity>(Predicate.OrElse(otherSpecification.Predicate));
        }

        public Specification<TEntity> Not()
        {
            if (Predicate == null) throw new InvalidOperationException();

            return new ReadOnlySpecification<TEntity>(Predicate.Not());
        }

        public static Specification<TEntity> False()
        {
            return new ReadOnlySpecification<TEntity>(_ => false);
        }

        public static Specification<TEntity> True()
        {
            return new ReadOnlySpecification<TEntity>(_ => true);
        }
    }
}