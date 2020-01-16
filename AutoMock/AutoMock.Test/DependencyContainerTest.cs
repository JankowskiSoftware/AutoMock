using System;
using NUnit.Framework;

namespace AutoMock.Test
{
    public class DependencyContainerTest
    {
        [TestCase(typeof(string), "value1", null, typeof(string), "value1", null)]
        [TestCase(typeof(string), "value1", null, typeof(string), "value2", null)]
        [TestCase(typeof(string), "value1", null, typeof(string), 1, null)]
        [TestCase(typeof(string), "value1", "sameParam", typeof(string), 1, "sameParam")]

        //[ExpectedException(typeof(InvalidOperationException))]
        [Test, Description("Should throw when adding same dependency1")]
        public void Should_throw_when_adding_same_dependency1(
            Type type1, object value1, string dependencyParameterName1,
            Type type2, object value2, string dependencyParameterName2)
        {
            //ARRANGE
            var dependencyContainer = new DependencyContainer();

            //ACT
            dependencyContainer.AddDependencyInstance(type1, value1, dependencyParameterName1);
            Assert.That(() => dependencyContainer.AddDependencyInstance(type2, value2, dependencyParameterName2), Throws.TypeOf<InvalidOperationException>());
        }

        [TestCase("sameValue", null, "sameValue", null)]
        [TestCase("sameValue", "sameParam", "sameValue", "sameParam")]

        //[ExpectedException(typeof(InvalidOperationException))]
        [Test, Description("Should throw when adding same dependency2")]
        public void Should_throw_when_adding_same_dependency2(
            object value1, string dependencyParameterName1,
            object value2, string dependencyParameterName2)
        {
            //ARRANGE
            var dependencyContainer = new DependencyContainer();

            //ACT
            dependencyContainer.AddDependencyInstance(value1, dependencyParameterName1);
            Assert.That(() => dependencyContainer.AddDependencyInstance(value2, dependencyParameterName2), Throws.TypeOf<InvalidOperationException>());
        }
    }
}
