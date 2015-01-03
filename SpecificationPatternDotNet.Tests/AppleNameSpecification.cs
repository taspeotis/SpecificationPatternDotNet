using System;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet.Tests
{
    internal sealed class AppleNameSpecification : Specification<Apple>
    {
        private readonly string _name;

        public AppleNameSpecification(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
                _name = name;
        }

        protected override Expression<Func<Apple, bool>> Predicate
        {
            get
            {
                if (_name == null)
                    return _ => true;

                return f => f.Name.Contains(_name);
            }
        }
    }
}