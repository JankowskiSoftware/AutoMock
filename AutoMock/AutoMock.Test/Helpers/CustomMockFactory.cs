using System;
using System.Linq;

namespace AutoMock.Test.Helpers
{
    class CustomMockFactory : IMockingFactory
    {
        public object CreateMock(Type dependencyType)
        {
            var constructorInfo = dependencyType.GetConstructors().Single();
            return constructorInfo.Invoke(new object[0]);
        }
    }
}