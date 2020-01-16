using System;

namespace AutoMock
{
    /// <summary>
    /// Creates mocks of specific type.
    /// </summary>
    public interface IMockingFactory
    {
        /// <summary>
        /// Creates mocks of specific type.
        /// </summary>
        /// <param name="dependencyType">Dependency type that will be injected ito target type.</param>
        /// <returns>Mocked dependency.</returns>
        object CreateMock(Type dependencyType);
    }
}
