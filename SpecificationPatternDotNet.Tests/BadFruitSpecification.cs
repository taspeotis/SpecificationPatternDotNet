using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet.Tests
{
    internal sealed class BadFruitSpecification : Specification<Fruit>
    {
        protected override Expression<Func<Fruit, bool>> Predicate
        {
            get { return f => f.Bad; }
        }
    }
}