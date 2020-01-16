using AutoMock.Samples.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AutoMock.Samples.InjectDependencyInstance
{
    [TestFixture]
    public class InjectDependencyInstance
    {
        [Test, Description("Should create mocks, build target and inject dependencies and mocks")]
        public void Should_create_mocks_build_target_and_inject_dependencies_and_mocks()
        {
            // ASSERT
            var container = new DependencyContainer();
            container.AddDependencyInstance(new FancyGloves());

            var builder = new AutoMock<Driver>(container);
            builder.SelectConstructor();
            var driver = builder.CreateTarget();

            //ACT
            driver.Drive();

            //ASSERT
            Assert.IsNotNull(builder.GetMock<IVehicle>());
            Assert.IsNotNull(builder.GetMock<IDrivingLicense>());

            builder.GetMock<IVehicle>()
                .Received()
                .Drive();
        }
    }

    class Driver
    {
        private readonly IGloves _gloves;
        private readonly IVehicle _vehicle;
        private readonly IDrivingLicense _drivingLicense;

        public Driver(IGloves gloves, IVehicle vehicle, IDrivingLicense drivingLicense)
        {
            _gloves = gloves;
            _vehicle = vehicle;
            _drivingLicense = drivingLicense;
        }

        public void Drive()
        {
            _vehicle.Drive();
        }
    }
}
