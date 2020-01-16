using System.Linq;
using System.Reflection;
using AutoMock.Samples.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AutoMock.Samples.ChoiceConstructorByFunction
{
    [TestFixture]
    public class ChoiceConstructorByFunction
    {
        [Test, Description("Should create mocks, build target and inject mocks to constructor selected by function")]
        public void Should_create_mocks_build_target_and_inject_mocks_to_constructor_selected_by_function()
        {
            // ASSERT
            var builder = new AutoMock<Driver>();
            builder.SelectConstructor(SelectConstructorFunc);
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

        private ConstructorInfo SelectConstructorFunc(ConstructorInfo[] constructorInfos)
        {
            return constructorInfos.First();
        }
    }

    class Driver
    {
        private readonly IGloves _gloves;
        private readonly IVehicle _vehicle;
        private readonly IDrivingLicense _drivingLicense;

        public Driver(IVehicle vehicle, IDrivingLicense drivingLicense)
        {
            _vehicle = vehicle;
            _drivingLicense = drivingLicense;
        }

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
