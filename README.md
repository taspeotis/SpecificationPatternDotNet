# SpecificationPatternDotNet

An implementation of the [Specification Pattern](http://en.wikipedia.org/wiki/Specification_pattern) using idiomatic C#.

## License

*SpecificationPatternDotNet* uses [code from MSDN](http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx) that is licensed under the [*Microsoft Limited Public License (MS-LPL)*](http://msdn.microsoft.com/en-us/cc300389.aspx#P). Consequently, *SpecificationPatternDotNet* has the same license.

## Getting Started

[Install the *SpecificationPatternDotNet* package from NuGet](http://nuget.org/packages/SpecificationPatternDotNet/).

### Creating Specifications

Simply derive from `Specification<TEntity>` and override `Predicate`.

```csharp
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
```

### Using Specifications

Call `SatisifiedBy` on your specification to use it.

```csharp
var entities = GetEntities();
var satisfiedEntities = compositeSpec.SatisfiedBy(entities);

foreach (var entity in satisfiedEntities)
    Console.WriteLine("Found one! {0}", entity);
```

### Combining Specifications

`Specification<TEntity>` contains `AndAlso` and `OrElse` methods for combining specifications.

```csharp
var greaterThanSpec = new GreaterThanSpec(2);
var lessThanSpec = new LessThanSpec(-2);
var compositeSpec = greaterThanSpec.OrElse(lessThanSpec);
```

The `True` and `False` methods create an instance of `Specification<TEntity>` that can serve as useful starting points for combining specifications.

```csharp
var specifications = GetSpecifications();
var allSpecifications = Specification<int>.True();

foreach (var specification in specifications)
    allSpecifications = allSpecifications.AndAlso(specification);
````

The `Not` method can be used to take the inverse of a specification.

## Miscellany

*SpecificationPatternDotNet* targets the .NET Framework 4.5, which is only available on Windows Vista and later.

Nothing prevents you from reducing the library target to a version of the .NET Framework that runs on operating systems earlier than Windows Vista.

The library is not tied to any particular architecture, the code should execute on x86, x64 and ia64.

## Bugs/Feedback

If you encounter a scenario where code doesn't work as you expect, you're welcome to report a bug, initiate a pull request or send an email to `t AT speot DOT is`. The latter method is likely to elicit a response, but not guaranteed.

The above applies equally for feedback.
