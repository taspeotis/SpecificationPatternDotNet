using System.Linq;

namespace SpecificationPatternDotNet
{
    public interface ISpecification<in TEntity>
    {
        IQueryable<TDerivedEntity> SatisfiedBy<TDerivedEntity>(IQueryable<TDerivedEntity> entities)
            where TDerivedEntity : TEntity;
    }
}