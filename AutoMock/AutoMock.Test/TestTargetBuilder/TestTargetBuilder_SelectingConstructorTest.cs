using System;
using AutoMock.Test.Helpers;
using NUnit.Framework;

namespace AutoMock.Test.TestTargetBuilder
{
    [TestFixture]
    public class TestTargetBuilder_SelectingConstructorTest
    {
        [Test, Description("Should build default target When parametrized constructor exists")]
        public void Should_build_default_target_When_parametrized_constructor_exists()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            builder.SelectDefaultConstructor();
            var target = builder.CreateTarget();

            //ASSERT
            Assert.AreEqual(typeof (Target), target.GetType());
        }

        [Test, Description("Should build parametrized target When default constructor")]
        public void Should_build_parametrized_target_When_default_constructor()
        {
            //ARRANGE
            var builder = new AutoMock<Target>();

            //ACT
            builder.SelectConstructor();
            var target = builder.CreateTarget();

            //ASSERT
            Assert.AreEqual(typeof(Target), target.GetType());
        }

        [Test, Description("Should call proper constructor When selecting by parameters number")]
        public void Should_call_proper_constructor_When_selecting_by_parameters_number()
        {
            //ARRANGE
            var builder = new AutoMock<Target_ConstructorsWithDifferentParametersCount>();

            //ACT
            builder.SelectConstructor(2);
            var target = builder.CreateTarget();

            //ASSERT
            Assert.IsNotNull(target);
        }



        [Test, Description("Should throw When default constructor does not exists")]
        public void Should_throw_When_default_constructor_does_not_exists()
        {
            //ARRANGE
            var builder = new AutoMock<Target_NoDefaultConstructor>();

            //ACT
            Assert.That(() => builder.SelectDefaultConstructor(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw When no constructor with dependencies")]
        public void Should_throw_When_no_constructor_with_dependencies()
        {
            //ARRANGE
            var builder = new AutoMock<Target_NoParametrizedConstructor>();

            //ACT
            Assert.That(() => builder.SelectConstructor(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw When more constructor with dependencies")]
        public void Should_throw_When_more_constructor_with_dependencies()
        {
            //ARRANGE
            var builder = new AutoMock<Target_ConstructorsWithDifferentParametersCount>();

            //ACT
            Assert.That(() => builder.SelectConstructor(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw When no constructor with chosen number of parameters")]
        public void Should_throw_When_no_constructor_with_chosen_number_of_parameters()
        {
            //ARRANGE
            var builder = new AutoMock<Target_ConstructorsWithDifferentParametersCount>();

            //ACT
            Assert.That(() => builder.SelectConstructor(999), Throws.TypeOf<InvalidOperationException>());
        }

        [Test, Description("Should throw When more then one constructor with chosen number of parameters")]
        public void Should_throw_When_more_then_one_constructor_with_chosen_number_of_parameters()
        {
            //ARRANGE
            var builder = new AutoMock<Target_MultipleConstructorsWithSameParametersCount>();

            //ACT
            Assert.That(() => builder.SelectConstructor(2), Throws.TypeOf<InvalidOperationException>());
        }
    }
}
