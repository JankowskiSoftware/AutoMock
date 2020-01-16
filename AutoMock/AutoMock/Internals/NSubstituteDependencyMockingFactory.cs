using System;
using NSubstitute;

namespace AutoMock.Internals
{
    public class NSubstituteMockingFactory : IMockingFactory
    {
        public object CreateMock(Type dependencyType)
        {
            try
            {
                return Substitute.For(new[] { dependencyType }, new object[0]);
            }
            catch (Exception ex)
            {
                const string messageFormat = "Unable to create substitute for type '{0}'. Inject the dependency instance in constructor or see inner exception for more details.";
                throw new Exception(String.Format(messageFormat, dependencyType), ex);
            }
        }
    }
}
