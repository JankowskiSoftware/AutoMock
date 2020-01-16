using AutoMock.Samples.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AutoMock.Samples.SimpleExample
{
    public class SimpleExample
    {
        [Test]
        public void Should_create_mocks_build_target_and_inject_mocks()
        {
            // ASSERT
            var builder = new AutoMock<Driver>();
            builder.SelectConstructor();
            var driver = builder.CreateTarget();

            //ACT
            driver.Drive();
            
            //ASSERT
            Assert.IsNotNull(builder.GetMock<IGloves>());
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
