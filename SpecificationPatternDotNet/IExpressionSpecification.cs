using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    public interface IExpressionSpecification
    {
        Expression Predicate { get; } 
    }
}