using AutoMock.Test.Helpers;
using NUnit.Framework;

namespace AutoMock.Test.TestTargetBuilder
{
    [TestFixture]
    public class TestTargetBuilder_CustomMockFactoryTest
    {
        [Test, Description("Should use custom mock factory")]
        public void Should_use_custom_mock_factory()
        {
            //ARRANGE
            var builder = new AutoMock<Target_ConcreteDependency>(new CustomMockFactory());

            //ACT
            builder.SelectConstructor();

            //ASSERT
            Assert.IsNotNull(builder.GetMock<DependencyImplementation1>());
        }

        [Test, Description("should use custom mock and dependency container")]
        public void should_use_custom_mock_and_dependency_container()
        {
            //ARRANGE
            var container = new DependencyContainer();
            container.AddDependencyInstance(new DependencyImplementation1());

            var builder = new AutoMock<Target_TwoConcreteDependencies>(new CustomMockFactory(), container);

            //ACT
            builder.SelectConstructor();

            //ASSERT
            Assert.IsNotNull(builder.GetMock<DependencyImplementation2>());
        }
    }
}