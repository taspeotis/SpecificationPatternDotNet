using System.Linq;

namespace SpecificationPatternDotNet
{
    public interface ISpecification<TEntity>
    {
        IQueryable<TEntity> SatisfiedBy(IQueryable<TEntity> entities);
    }
}
