using System.Data.Entity;

namespace SpecificationPatternDotNet.Tests
{
    internal sealed class FruitContext : DbContext
    {
        public DbSet<Fruit> Fruits { get; set; }

        public DbSet<Apple> Apples { get; set; }

        static FruitContext()
        {
            Database.SetInitializer(new GrowFruitFromSeedsInitializer());
        }
    }
}