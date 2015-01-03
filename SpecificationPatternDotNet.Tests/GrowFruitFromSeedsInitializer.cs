using System.Data.Entity;

namespace SpecificationPatternDotNet.Tests
{
    internal sealed class GrowFruitFromSeedsInitializer : DropCreateDatabaseAlways<FruitContext>
    {
        protected override void Seed(FruitContext context)
        {
            base.Seed(context);

            context.Apples.Add(new Apple {FruitId = 10, Bad = false, Name = "Jonathan"});
            context.Apples.Add(new Apple {FruitId = 11, Bad = true, Name = "Royal Gala"});
            context.Apples.Add(new Apple {FruitId = 12, Bad = false, Name = "Golden Delicious"});
            context.Apples.Add(new Apple {FruitId = 13, Bad = true, Name = "Red Delicious"});
            context.Apples.Add(new Apple {FruitId = 14, Bad = false, Name = "Jonagold"});
            context.Apples.Add(new Apple {FruitId = 15, Bad = true, Name = "Fuji"});
            context.Apples.Add(new Apple {FruitId = 16, Bad = false, Name = "Braeburn"});
        }
    }
}