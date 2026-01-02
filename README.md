# AutoMock

AutoMock solves a critical problem in unit testing: **automatically adapting your tests when class constructor dependencies change**. 

When writing unit tests, you need to mock all dependencies of the class you're testing. Typically, you manually create these mocks and inject them into your class constructor. However, when your class evolves and its constructor parameters change, you have to manually update every single test that creates that class. AutoMock eliminates this tedious work by automatically discovering and mocking all dependencies at runtime.

## The Problem: Manual Dependency Management

Let's consider a simple example:

```csharp
    class Car
    {
        private readonly IEngine _engine;

        public Car(IEngine engine)
        {
            _engine = engine;
        }

        public void Drive()
        {
            _engine.Start();
        }
    }
 ```

### Writing a Test the Manual Way

You need to mock the `IEngine` dependency and inject it:

```csharp
[Test]
void ShouldStartEngine()
{
    // ARRANGE - Manually create mocks and pass to constructor
    IEngine engine = Substitute.For<IEngine>();
    
    Car car = new Car(engine);

    // ACT
    car.Drive();

    // ASSERT
    engine
        .Received()
        .Start();
}
```

This works fine initially, but what happens when your class evolves?

### AutoMock: Automatic Dependency Discovery

```csharp
[Test]
public void ShouldStartEngine()
{
    // ARRANGE - AutoMock automatically discovers and mocks all dependencies
    var mock = new AutoMock<Car>();
    mock.SelectConstructor();                // Discover all constructor dependencies
    
    Car car = mock.CreateTarget();           // Car instance with all mocks injected
    
    // ACT
    car.Drive();

    // ASSERT
    // Gets the automatically created mock
    var engineMock = mock.GetMock<IEngine>()
        
    // examin the expected result
    engineMock.Received()
        .Start();
}
```

Notice what happens: **Even when the `Car` constructor changes to require `ILights` and `IDoors`, this test doesn't need any modifications!** AutoMock automatically handles the new dependencies.

## How AutoMock Works

1. **`new AutoMock<Car>()`** - Creates an AutoMock container for the `Car` class
2. **`SelectConstructor()`** - Examines the `Car` constructor and identifies all its dependencies (`IEngine`, `ILights`, `IDoors`, etc.)
3. **AutoMock creates mocks** - Automatically creates mock implementations for each dependency
4. **`CreateTarget()`** - Instantiates a `Car` object, injecting all the mocks into the constructor
5. **`GetMock<T>()`** - Retrieves a specific mock when you need to verify its behavior

## Why This Matters

| Approach | Adding a new dependency |
|----------|----------------------|
| **Manual Mocking** | Update every test that creates the class ❌ |
| **AutoMock** | No test changes needed ✅ |

As your codebase grows with hundreds of tests, this small difference compounds into massive time savings and fewer bugs.

## Key Benefits

- **Automatic dependency discovery** - No manual mock setup
- **Constructor-agnostic** - Tests work regardless of parameter count
- **Refactoring-friendly** - Change constructors without breaking tests
- **Less boilerplate** - Write tests faster with less code
- **Backward compatible** - Works with any mocking framework

## Common Use Cases

- Testing classes with multiple dependencies
- Refactoring code while maintaining tests
- Building test suites that are resilient to design changes
- Reducing repetitive mock setup code

# Samples and More Examples

You will find additional examples of how to use AutoMock in the `AutoMock.Samples` project, including:
- Simple dependency injection scenarios
- Selecting specific constructors
- Injecting custom dependency instances
- Using custom mock factories