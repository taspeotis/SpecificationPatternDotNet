using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet.Tests
{
    internal class LessThanSpec : Specification<int>
    {
        private readonly int _value;

        public LessThanSpec(int value)
        {
            _value = value;
        }

        protected override Expression<Func<int, bool>> Predicate
        {
            get { return i => i < _value; }
        }
    }
}