using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecificationPatternDotNet.Tests
{
    [TestClass]
    public class EntitySpecificationTests
    {
        [TestMethod]
        public void EntitySpecification_Can_Be_Used_With_DerivedEntitySpecification()
        {
            using (var fruitContext = new FruitContext())
            {
                var badFruitSpecification = new BadFruitSpecification();
                var appleNameSpecification = new AppleNameSpecification("R");

                var satisfiedApples = badFruitSpecification
                    .AndAlso(appleNameSpecification)
                    .SatisfiedBy(fruitContext.Apples);

                Assert.IsTrue(satisfiedApples.Any());
                Assert.IsTrue(satisfiedApples.All(a => a.Bad && a.Name.Contains("R")));
            }
        }

        [TestMethod]
        public void EntityPredicate_Can_Be_Used_With_DerivedEntityQueryable()
        {
            using (var fruitContext = new FruitContext())
            {
                var badFruitSpecification = new BadFruitSpecification();

                var satisfiedApples = badFruitSpecification.SatisfiedBy(fruitContext.Apples);

                Assert.IsTrue(satisfiedApples.Any());
            }
        }
    }
}
