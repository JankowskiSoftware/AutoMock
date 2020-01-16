# AutoMock

This library is very useful when writing Unit Tests. It automatically mocks all dependencies of the tested class. 

Lets consider following example:

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

 It contains one dependency that needs do be mocked e.g. like this:

```csharp
    [Test]
    void ShouldStartEngine()
    {
        // ARRANGE
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

As development goes the number of dependencies changes frequently causing build errors and lots of work with fixing tests e.g.:

```csharp
    class Car
    {
        private readonly IEngine _engine;
        private readonly ILights _lights;
        private readonly IDors _dors;

        public Car(IEngine engine, ILights lights, IDors dors)
        {
            _engine = engine;
            _lights = lights;
            _dors = dors;
        }

        public void Drive()
        {
            _engine.Start();
        }
    }
 ```

 In that case You would need to fix all tests that create the Car object.   
 However when using AutoMock witch is discovering all dependencies at run time you would not need to fix anything.

 Below you will find the same test written using AutoMock:

```csharp 
    [Test]
    public void ShouldRun()
    {
        // ARRANGE
        var mock = new AutoMock<Car>(); // Create AutoMock
        mock.SelectConstructor();       // Discover all dependencies from default constructor

        Car = mock.CreateTarget();      // Create object Car with mocked dependencies

        // ACT
        car.Drive();

        // ASSERT
        mock.GetMock<IEngine>()         // Get the mock that was automatically discovered and injected
            .Received()
            .Start();
    }
 ```

# Samples 
You will find more examples of how to use this library in AutoMock.Samples.SimpleExample project.