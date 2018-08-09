# SpecificationPatternDotNet

An implementation of the [Specification Pattern](http://en.wikipedia.org/wiki/Specification_pattern) using idiomatic C#.

## License

*SpecificationPatternDotNet* is licensed under the [*Microsoft Public License (MS-PL)*](http://www.microsoft.com/en-us/openness/licenses.aspx).

## Getting Started

[Install the *SpecificationPatternDotNet* package from NuGet](http://nuget.org/packages/SpecificationPatternDotNet/).

### Creating Specifications

Simply derive from `Specification<TEntity>` and override `Predicate`.

```csharp
class GreaterThanSpecification : Specification<int>
{
    private readonly int _value;

    public GreaterThanSpecification(int value)
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
var satisfiedEntities = compositeSpecification.SatisfiedBy(entities);

foreach (var entity in satisfiedEntities)
    Console.WriteLine("Found one! {0}", entity);
```

### Combining Specifications

`Specification<TEntity>` contains `AndAlso` and `OrElse` methods for combining specifications.

```csharp
var greaterThanSpecification = new GreaterThanSpecification(-2);
var lessThanSpecification = new LessThanSpecification(2);
var compositeSpecification = greaterThanSpecification.AndAlso(lessThanSpecification);
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

*SpecificationPatternDotNet* targets .NET Standard 2.0.

The library is not tied to any particular architecture, the code should execute on x86, x64 and ia64.

## Bugs/Feedback

If you encounter a scenario where code doesn't work as you expect, you're welcome to report a bug, initiate a pull request or send an email to `t AT speot DOT is`. The latter method is likely to elicit a response, but not guaranteed.

The above applies equally for feedback.
