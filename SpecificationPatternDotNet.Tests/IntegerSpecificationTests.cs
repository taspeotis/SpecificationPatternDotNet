using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecificationPatternDotNet.Tests
{
    [TestClass]
    public class IntegerSpecificationTests
    {
        [TestMethod]
        public void AndAlso_Satisfies_Both()
        {
            var greaterThanSpecification = new GreaterThanSpecification(-2);
            var lessThanSpecification = new LessThanSpecification(2);
            var compositeSpecification = greaterThanSpecification.AndAlso(lessThanSpecification);

            using (var fruitContext = new FruitContext())
            {
                var integers = GetIntegers(fruitContext);
                var satisfiedIntegers = compositeSpecification.SatisfiedBy(integers);

                Assert.IsTrue(satisfiedIntegers.Any());
                Assert.IsTrue(satisfiedIntegers.All(i => i > -2 && i < 2));
            }
        }

        [TestMethod]
        public void OrElse_Satisfies_Either()
        {
            var greaterThanSpecification = new GreaterThanSpecification(2);
            var lessThanSpecification = new LessThanSpecification(-2);
            var compositeSpecification = greaterThanSpecification.OrElse(lessThanSpecification);

            using (var fruitContext = new FruitContext())
            {
                var integers = GetIntegers(fruitContext);
                var satisfiedIntegers = compositeSpecification.SatisfiedBy(integers);

                Assert.IsTrue(satisfiedIntegers.Any());
                Assert.IsTrue(satisfiedIntegers.All(i => i > 2 || i < -2));
            }
        }

        [TestMethod]
        public void Not_Satisfies_Inverse()
        {
            var greaterThanSpecification = new GreaterThanSpecification(2);
            var lessThanSpecification = new LessThanSpecification(-2);
            var compositeSpecification = greaterThanSpecification.OrElse(lessThanSpecification);
            var inverseSpecification = compositeSpecification.Not();

            using (var fruitContext = new FruitContext())
            {
                var integers = GetIntegers(fruitContext);
                var satisfiedIntegers = inverseSpecification.SatisfiedBy(integers);

                Assert.IsTrue(satisfiedIntegers.Any());
                Assert.IsTrue(satisfiedIntegers.All(i => !(i > 2 || i < -2)));
            }
        }

        [TestMethod]
        public void False_Satisfies_None()
        {
            var falseSpecification = Specification<int>.False();

            using (var fruitContext = new FruitContext())
            {
                var integers = GetIntegers(fruitContext);
                var satisfiedIntegers = falseSpecification.SatisfiedBy(integers);

                Assert.IsTrue(!satisfiedIntegers.Any());
            }
        }

        [TestMethod]
        public void True_Satisfies_All()
        {
            var trueSpecification = Specification<int>.True();

            using (var fruitContext = new FruitContext())
            {
                var integers = GetIntegers(fruitContext);
                var satisfiedIntegers = trueSpecification.SatisfiedBy(integers);

                Assert.IsTrue(satisfiedIntegers.Any());
                CollectionAssert.AreEquivalent(integers.ToList(), satisfiedIntegers.ToList());
            }
        }

        private static IQueryable<int> GetIntegers(FruitContext fruitContext)
        {
            return fruitContext.Fruits.Select(f => f.FruitId - 13);
        }
    }
}