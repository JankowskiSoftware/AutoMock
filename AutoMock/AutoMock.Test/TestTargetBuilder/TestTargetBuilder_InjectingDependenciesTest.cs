using System;
using AutoMock.Test.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AutoMock.Test.TestTargetBuilder
{
    [TestFixture]
    public class TestTargetBuilder_InjectingDependenciesTest
    {
        [Test, Description("Should build target When not provided dependencies")]
        public void Should_build_target_When_not_provided_dependencies()
        {
            var builder = new AutoMock<Target>();

            //ACT
            builder.SelectConstructor();
            var target = builder.CreateTarget();

            //ASSERT
            Assert.IsNotNull(target);
        }

        [Test, Description("Should build target When provided dependencies")]
        public void Should_build_target_When_provided_dependencies()
        {
            //ASSERT
            var container = new DependencyContainer();
            container.AddDependencyInstance(Substitute.For<IDependency>());
            var builder = new AutoMock<Target>(container);

            //ACT
            builder.SelectConstructor();
            var target = builder.CreateTarget();

            //ASSERT
            Assert.IsNotNull(target);
        }

        [Test, Description("Should throw exception When no dependency provided for value type")]
        public void Should_throw_exception_When_no_dependency_provided_for_value_type()
        {
            var builder = new AutoMock<Target_ValueTypeDependency>();

            //ACT
            Assert.That(() => builder.SelectConstructor(), Throws.TypeOf<Exception>());
        }

        [Test, Description("Should build target When value dependency")]
        public void Should_build_target_When_value_dependency()
        {
            //ARRANGE
            var container = new DependencyContainer();
            container.AddDependencyInstance(true);
            container.AddDependencyInstance(new Byte());
            container.AddDependencyInstance('c');
            container.AddDependencyInstance(new Decimal());
            container.AddDependencyInstance((double)1);
            container.AddDependencyInstance((float)1);
            container.AddDependencyInstance((int)1);
            container.AddDependencyInstance((long)1);
            container.AddDependencyInstance((sbyte)1);
            container.AddDependencyInstance((short)1);
            container.AddDependencyInstance((uint)1);
            container.AddDependencyInstance((ulong)1);
            container.AddDependencyInstance((ushort)1);
            container.AddDependencyInstance("string");

            var builder = new AutoMock<Target_ValueTypeDependency>(container);

            //ACT
            builder.SelectConstructor();
            var target = builder.CreateTarget();

            //ASSERT
            Assert.IsNotNull(target);
        }

        [Test, Description("Should build target When multiple dependencies of the same type")]
        public void Should_build_target_When_multiple_dependencies_of_the_same_type()
        {
            //ARRANGE
            var container = new DependencyContainer();
            container.AddDependencyInstance(Substitute.For<IDependency>(), "d1");
            container.AddDependencyInstance(Substitute.For<IDependency>(), "d2");
            var builder = new AutoMock<Target_SameTypeDependency>(container);

            //ACT
            builder.SelectConstructor();
            var target = builder.CreateTarget();

            //ASSERT
            Assert.IsNotNull(target);
        }
    }
}