using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMock.Internals
{
    internal class AutoMockCore<TTargetType> where TTargetType : class
    {
        private IMockingFactory _mockingFactory;

        public Type TargetType { get; private set; }

        public DependencyContainer DependencyContainer { get; private set; }
        public DependencyContainer MocksContainer { get; private set; }

        private void initialise()
        {
            TargetType = typeof(TTargetType);
            MocksContainer = new DependencyContainer();
            DependencyContainer = new DependencyContainer();
            _mockingFactory = new NSubstituteMockingFactory();
        }

        public AutoMockCore()
        {
            initialise();
        }

        public AutoMockCore(IMockingFactory mockingFactory)
        {
            initialise();

            _mockingFactory = mockingFactory;
        }

        public AutoMockCore(DependencyContainer dependencyContainer)
        {
            initialise();

            DependencyContainer = dependencyContainer;
        }

        public AutoMockCore(IMockingFactory mockingFactory, DependencyContainer dependencyContainer)
        {
            initialise();

            _mockingFactory = mockingFactory;
            DependencyContainer = dependencyContainer;
        }


        public List<object> CompileParametersForSelectedConstructor(ConstructorInfo constructor)
        {
            MocksContainer.Clear();

            var parametersList = new List<object>();
            foreach (var parameterInfo in constructor.GetParameters())
            {
                object dependency = FindDependency(parameterInfo);
                if (dependency != null)
                {
                    parametersList.Add(dependency);
                    continue;
                }

                var mock = _mockingFactory.CreateMock(parameterInfo.ParameterType);
                
                parametersList.Add(mock);
                MocksContainer.AddDependencyInstance(parameterInfo.ParameterType, mock, parameterInfo.Name);
            }

            foreach (var item in DependencyContainer)
            {
                MocksContainer.AddDependencyInstance(item.Type, item.Value, item.ConstructorPropertyName);

            }


            return parametersList;
        }

        private object FindDependency(ParameterInfo parameterInfo)
        {
            if (DependencyContainer.ContainsDependencyImplementation(parameterInfo.ParameterType, parameterInfo.Name))
                return DependencyContainer.GetParentTypeDependency(parameterInfo.ParameterType, parameterInfo.Name);

            if (DependencyContainer.ContainsDependencyImplementation(parameterInfo.ParameterType))
                return DependencyContainer.GetParentTypeDependency(parameterInfo.ParameterType);

            return null;
        }
    }
}