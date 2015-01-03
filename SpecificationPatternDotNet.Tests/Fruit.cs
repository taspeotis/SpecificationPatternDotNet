using System.ComponentModel.DataAnnotations.Schema;

namespace SpecificationPatternDotNet.Tests
{
    internal class Fruit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FruitId { get; set; }

        public bool Bad { get; set; }
    }
}