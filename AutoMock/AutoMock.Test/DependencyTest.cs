using NUnit.Framework;

namespace AutoMock.Test
{
    [TestFixture]
    public class DependencyTest
    {
        [Test, Description("Shold have the same hash code When same type and parameter name")]
        public void Shold_have_the_same_hash_code_When_same_type_and_parameter_name()
        {
            //ARRANGE
            var dependency1 = new Dependency(typeof(string), "value", "parameterName");
            var dependency2 = new Dependency(typeof(string), 2, "parameterName");
            
            //ASSERT
            Assert.AreEqual(dependency1.GetHashCode(), dependency2.GetHashCode());
        }

        [Test, Description("Should have different hash code When different type")]
        public void Should_have_different_hash_code_When_different_type()
        {
            //ARRANGE
            var dependency1 = new Dependency(typeof(string), "value", "parameterName");
            var dependency2 = new Dependency(typeof(int), 2, "parameterName");

            //ASSERT
            Assert.AreNotEqual(dependency1.GetHashCode(), dependency2.GetHashCode());
        }

        [Test, Description("Should have different hash code When different parameter name")]
        public void Should_have_different_hash_code_When_different_parameter_name()
        {
            //ARRANGE
            var dependency1 = new Dependency(typeof(string), "value", "parameterName1");
            var dependency2 = new Dependency(typeof(string), 2, "parameterName2");

            //ASSERT
            Assert.AreNotEqual(dependency1.GetHashCode(), dependency2.GetHashCode());
        }

    }
}
