using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet.Tests
{
    internal class GreaterThanSpecification : Specification<int>
    {
        private readonly int _value;

        public GreaterThanSpecification(int value)
        {
            _value = value;
        }

        protected override Expression<Func<int, bool>> Predicate
        {
            get { return i => i > _value; }
        }
    }
}