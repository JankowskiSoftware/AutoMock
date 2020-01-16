using System;
using AutoMock.Test.Helpers;
using NUnit.Framework;

namespace AutoMock.Test.TestTargetBuilder
{
    [TestFixture]
    public class TestTargetBuilder_GetingCreatedMocksTest
    {
        [Test, Description("Should throw When get mock called before Building target1")]
        public void Should_throw_When_get_mock_called_before_Building_target1()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            Assert.That(() => builder.GetMock<Exception>(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw When GetMock called before Building target2")]
        public void Should_throw_When_GetMock_called_before_Building_target2()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            Assert.That(() => builder.GetMock<Exception>("parameterName"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should get injected mock")]
        public void Should_get_injected_mock()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            builder.SelectConstructor();

            //ASSERT
            Assert.IsNotNull(builder.GetMock<IDependency>());
        }

        [Test, Description("Should get injected mock When multiple dependencies of the same type")]
        public void Should_get_injected_mock_When_multiple_dependencies_of_the_same_type()
        {
            //ARRANGE
            var builder = new AutoMock<Target_SameTypeDependency>();

            //ACT
            builder.SelectConstructor();

            //ASSERT
            Assert.IsNotNull(builder.GetMock<IDependency>("d1"));
            Assert.IsNotNull(builder.GetMock<IDependency>("d2"));
        }

        [Test, Description("Should throw when getting not created mock type")]
        public void Should_throw_when_getting_not_created_mock_type()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            builder.SelectConstructor();
            Assert.That(() => builder.GetMock<IDependency1>(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw when getting not created mock parameter name")]
        public void Should_throw_when_getting_not_created_mock_parameter_name()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            builder.SelectConstructor();
            Assert.That(() => builder.GetMock<IDependency>("notExistingName"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw When getting dependency registered under multiple names")]
        public void Should_throw_When_getting_dependency_registered_under_multiple_names()
        {
            //ARRANGE
            var builder = new AutoMock<Target_SameTypeDependency>();

            //ACT
            builder.SelectConstructor();
            Assert.That(() => builder.GetMock<IDependency>(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw when two implementations of the same interface injected")]
        public void Should_throw_when_two_implementations_of_the_same_interface_injected()
        {
            //ARRANGE
            var container = new DependencyContainer();
            container.AddDependencyInstance(new DependencyImplementation1());
            container.AddDependencyInstance(new DependencyImplementation2());
            
            var builder = new AutoMock<Target>(container);

            //ACT
            Assert.That(() => builder.SelectConstructor(), Throws.TypeOf<InvalidOperationException>());
        }
    }
}