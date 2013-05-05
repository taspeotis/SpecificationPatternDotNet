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
            var greaterThanSpecification = new GreaterThanSpecification(-2);
            var lessThanSpecification = new LessThanSpecification(2);
            var compositeSpecification = greaterThanSpecification.AndAlso(lessThanSpecification);

            var entities = GetEntities();
            var satisfiedEntities = compositeSpecification.SatisfiedBy(entities);

            Assert.IsTrue(satisfiedEntities.All(i => i > -2 && i < 2));
        }

        [TestMethod]
        public void OrElseSatisfiesEither()
        {
            var greaterThanSpecification = new GreaterThanSpecification(2);
            var lessThanSpecification = new LessThanSpecification(-2);
            var compositeSpecification = greaterThanSpecification.OrElse(lessThanSpecification);

            var entities = GetEntities();
            var satisfiedEntities = compositeSpecification.SatisfiedBy(entities);

            Assert.IsTrue(satisfiedEntities.All(i => i > 2 || i < -2));
        }

        [TestMethod]
        public void NotSatisfiesInverse()
        {
            var greaterThanSpecification = new GreaterThanSpecification(2);
            var lessThanSpecification = new LessThanSpecification(-2);
            var compositeSpecification = greaterThanSpecification.OrElse(lessThanSpecification);
            var inverseSpecification = compositeSpecification.Not();

            var entities = GetEntities();
            var satisfiedEntities = inverseSpecification.SatisfiedBy(entities);

            Assert.IsTrue(satisfiedEntities.All(i => !(i > 2 || i < -2)));
        }

        [TestMethod]
        public void FalseSatisfiesNone()
        {
            var falseSpecification = Specification<int>.False();

            var entities = GetEntities();
            var satisfiedEntities = falseSpecification.SatisfiedBy(entities);

            Assert.IsTrue(!satisfiedEntities.Any());
        }

        [TestMethod]
        public void TrueSatisfiesAll()
        {
            var trueSpecification = Specification<int>.True();

            var entities = GetEntities();
            var satisfiedEntities = trueSpecification.SatisfiedBy(entities);

            CollectionAssert.AreEquivalent(entities.ToList(), satisfiedEntities.ToList());
        }

        private static IQueryable<int> GetEntities()
        {
            return new[] {-3, -2, -1, 0, 1, 2, 3}.AsQueryable();
        }
    }
}