using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecificationPatternDotNet.Tests
{
    [TestClass]
    public class SpecificationTests
    {
        [TestMethod]
        public void AndAlsoSatisfiesBoth()
        {
            var greaterThanSpec = new GreaterThanSpec(-2);
            var lessThanSpec = new LessThanSpec(2);
            var compositeSpec = greaterThanSpec.AndAlso(lessThanSpec);

            var entities = GetEntities();
            var satisfiedEntities = compositeSpec.SatisfiedBy(entities);

            Assert.IsTrue(satisfiedEntities.All(i => i > -2 && i < 2));
        }

        [TestMethod]
        public void OrElseSatisfiesEither()
        {
            var greaterThanSpec = new GreaterThanSpec(2);
            var lessThanSpec = new LessThanSpec(-2);
            var compositeSpec = greaterThanSpec.OrElse(lessThanSpec);

            var entities = GetEntities();
            var satisfiedEntities = compositeSpec.SatisfiedBy(entities);

            Assert.IsTrue(satisfiedEntities.All(i => i > 2 || i < -2));
        }

        [TestMethod]
        public void NotSatisfiesInverse()
        {
            var greaterThanSpec = new GreaterThanSpec(2);
            var lessThanSpec = new LessThanSpec(-2);
            var compositeSpec = greaterThanSpec.OrElse(lessThanSpec);
            var inverseSpec = compositeSpec.Not();

            var entities = GetEntities();
            var satisfiedEntities = inverseSpec.SatisfiedBy(entities);

            Assert.IsTrue(satisfiedEntities.All(i => !(i > 2 || i < -2)));
        }

        [TestMethod]
        public void FalseSatisfiesNone()
        {
            var falseSpec = Specification<int>.False();

            var entities = GetEntities();
            var satisfiedEntities = falseSpec.SatisfiedBy(entities);

            Assert.IsTrue(!satisfiedEntities.Any());
        }

        [TestMethod]
        public void TrueSatisfiesAll()
        {
            var trueSpec = Specification<int>.True();

            var entities = GetEntities();
            var satisfiedEntities = trueSpec.SatisfiedBy(entities);

            CollectionAssert.AreEquivalent(entities.ToList(), satisfiedEntities.ToList());
        }

        private static IQueryable<int> GetEntities()
        {
            return new[] {-3, -2, -1, 0, 1, 2, 3}.AsQueryable();
        }
    }
}