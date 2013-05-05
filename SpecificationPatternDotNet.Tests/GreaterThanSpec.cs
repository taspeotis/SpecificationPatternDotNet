using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet.Tests
{
    internal class GreaterThanSpec : Specification<int>
    {
        private readonly int _value;

        public GreaterThanSpec(int value)
        {
            _value = value;
        }

        protected override Expression<Func<int, bool>> Predicate
        {
            get { return i => i > _value; }
        }
    }
}