using System;
using System.Linq;
using System.Reflection;
using AutoMock.Internals;

namespace AutoMock
{
    public class AutoMock<TTargetType> where TTargetType : class
    {
        private readonly AutoMockCore<TTargetType> TargetBuilder;

        private bool _targetResolved;

        private object[] _constructorParameters;
        private ConstructorInfo _selectedConstructor;

        public AutoMock()
        {
            TargetBuilder = new AutoMockCore<TTargetType>();
        }

        public AutoMock(DependencyContainer dependencyContainer)
        {
            TargetBuilder = new AutoMockCore<TTargetType>(dependencyContainer);
        }

        public AutoMock(IMockingFactory mockingFactory)
        {
            TargetBuilder = new AutoMockCore<TTargetType>(mockingFactory);            
        }

        public AutoMock(IMockingFactory mockingFactory, DependencyContainer dependencyContainer)
        {
            TargetBuilder = new AutoMockCore<TTargetType>(mockingFactory, dependencyContainer);            
        }

        public void SelectDefaultConstructor()
        {
            SelectConstructor(constructorInfos =>
            {
                var constructors = constructorInfos
                   .Where(ctorInfo => ctorInfo.GetParameters().None())
                   .ToArray();

                if (constructors.None())
                    throw new InvalidOperationException(String.Format("Object {0} does not contain default constructor.", TargetBuilder.TargetType));

                return constructors.Single();
            });
        }

        public void SelectConstructor()
        {
            SelectConstructor(constructorInfos =>
            {
                var constructors = constructorInfos
                    .Where(ctorInfo => ctorInfo.GetParameters().Any())
                    .ToArray();

                if (constructors.None())
                    throw new InvalidOperationException(String.Format("Object {0} does not contain constructor with dependencies.", TargetBuilder.TargetType));

                if (constructors.Count() > 1)
                    throw new InvalidOperationException(String.Format("Object {0} contains more than one constructor with dependencies.", TargetBuilder.TargetType));

                return constructors.Single();
            });
        }

        public void SelectConstructor(int dependenciesNumber)
        {
            SelectConstructor(constructorInfos =>
            {
                var constructors = constructorInfos
                    .Where(ctorInfo => ctorInfo.GetParameters().Count() == dependenciesNumber)
                    .ToArray();

                if (constructors.None())
                    throw new InvalidOperationException(String.Format("Object {0} does not contain constructor with {1} dependencies.", TargetBuilder.TargetType, dependenciesNumber));

                if (constructors.Count() > 1)
                    throw new InvalidOperationException(String.Format("Object {0} contains more than one constructor with {1} dependencies.", TargetBuilder.TargetType, dependenciesNumber));

                return constructors.Single();
            });
        }

        public void SelectConstructor(Func<ConstructorInfo[], ConstructorInfo> selectConstructorFunc)
        {
            _selectedConstructor = selectConstructorFunc(typeof(TTargetType).GetConstructors());

            _constructorParameters = TargetBuilder
                .CompileParametersForSelectedConstructor(_selectedConstructor)
                .ToArray();

            _targetResolved = true;
        }

        public TTargetType CreateTarget()
        {
            return _selectedConstructor.Invoke(_constructorParameters) as TTargetType;
        }

        public T GetMock<T>() where T : class
        {
            if (!_targetResolved)
                throw new InvalidOperationException("Target was not resolved. There are no mocks created.");

            var type = typeof(T);

            return (T)TargetBuilder.MocksContainer.GetDependency(type);
        }

        public T GetMock<T>(string propertyName) where T : class
        {
            if (!_targetResolved)
                throw new InvalidOperationException("Target was not resolved. There are no mocks created.");

            var type = typeof(T);

            return TargetBuilder.MocksContainer.GetDependency(type, propertyName) as T;
        }
    }
}
