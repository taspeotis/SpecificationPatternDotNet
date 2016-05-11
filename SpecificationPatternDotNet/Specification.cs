using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    public abstract class Specification<TEntity> : IExpressionSpecification
    {
        protected abstract Expression<Func<TEntity, bool>> Predicate { get; }

        Expression IExpressionSpecification.Predicate => Predicate;

        public IQueryable<TDerivedEntity> SatisfiedBy<TDerivedEntity>(IQueryable<TDerivedEntity> entities)
            where TDerivedEntity : TEntity
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var entityPredicate = Predicate;

            if (entityPredicate == null) throw new InvalidOperationException("Predicate");

            if (typeof (TDerivedEntity) == typeof (TEntity))
                return (IQueryable<TDerivedEntity>) ((IQueryable<TEntity>) entities).Where(entityPredicate);

            var entityParameter = entityPredicate.Parameters.Single();
            var derivedEntityParameter = Expression.Parameter(typeof (TDerivedEntity), entityParameter.Name);

            var parameterVisitor = new ParameterVisitor(entityParameter, derivedEntityParameter);
            var derivedEntityBody = parameterVisitor.Visit(entityPredicate.Body);
            var derivedEntityExpression = Expression.Lambda(derivedEntityBody, derivedEntityParameter);

            return entities.Where((Expression<Func<TDerivedEntity, bool>>) derivedEntityExpression);
        }

        public IEnumerable<TDerivedEntity> SatisfiedBy<TDerivedEntity>(IEnumerable<TDerivedEntity> entities)
            where TDerivedEntity : TEntity
        {
            return SatisfiedBy(entities.AsQueryable());
        }

        public Specification<TDerivedEntity> AndAlso<TDerivedEntity>(Specification<TDerivedEntity> otherSpecification)
            where TDerivedEntity : TEntity
        {
            if (otherSpecification == null) throw new ArgumentNullException(nameof(otherSpecification));

            var entityPredicate = Predicate;

            if (entityPredicate == null) throw new InvalidOperationException();

            return new ReadOnlySpecification<TDerivedEntity>(entityPredicate.AndAlso(otherSpecification.Predicate));
        }

        public Specification<TDerivedEntity> OrElse<TDerivedEntity>(Specification<TDerivedEntity> otherSpecification)
            where TDerivedEntity : TEntity
        {
            if (otherSpecification == null) throw new ArgumentNullException(nameof(otherSpecification));

            var entityPredicate = Predicate;

            if (entityPredicate == null) throw new InvalidOperationException();

            return new ReadOnlySpecification<TDerivedEntity>(entityPredicate.OrElse(otherSpecification.Predicate));
        }

        public Specification<TEntity> Not()
        {
            var entityPredicate = Predicate;

            if (entityPredicate == null) throw new InvalidOperationException();

            return new ReadOnlySpecification<TEntity>(entityPredicate.Not());
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