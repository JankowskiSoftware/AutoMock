/*********************************  Header ********************************\
Author:       Michal Jankowski - www.mjankowski.org
Project:      AutoMock
Copyright:    (c)2015, mjankowski.org

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/en-us/openness/licenses.aspx.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMock.Internals;

namespace AutoMock
{
    /// <summary>
    /// Dependencies container.
    /// Stores dependencies by their type and name.
    /// </summary>
    public class DependencyContainer : IEnumerable<Dependency>
    {
        private readonly HashSet<Dependency> Container = new HashSet<Dependency>();

        /// <summary>
        /// Register instance of the dependency.
        /// </summary>
        /// <param name="dependency">Instance of dependency and dependency registration type.</param>
        /// <param name="dependencyName">Name of parameter from constructor.</param>
        public void AddDependencyInstance(object dependency, string dependencyName = null)
        {
            var dependencyDescription = new Dependency(dependency, dependencyName);
            if (Container.Contains(dependencyDescription))
                throw new InvalidOperationException(String.Format("This dependency is already registered: {0}", dependencyDescription));

            Container.Add(dependencyDescription);
        }


        /// <summary>
        /// Register instance of the dependency.
        /// </summary>
        /// <param name="type">Dependency registration type.</param>
        /// <param name="dependency">Instance of dependency.</param>
        /// <param name="dependencyName">Name of parameter from constructor.</param>
        public void AddDependencyInstance(Type type, object dependency, string dependencyName = null)
        {
            var dependencyDescription = new Dependency(type, dependency, dependencyName);
            if (Container.Contains(dependencyDescription))
                throw new InvalidOperationException(String.Format("This dependency is already registered: {0}", dependencyDescription));

            Container.Add(dependencyDescription);
        }



        internal object GetDependency(Type dependencyType)
        {
            var dependencies = Container.Where(w => w.Type == dependencyType);

            if (dependencies.None())
                throw new InvalidOperationException(String.Format("Dependency of type {0} was not registered.", dependencyType));

            if (dependencies.Count() > 1)
                throw new InvalidOperationException(String.Format("Dependency of type {0} is registered under multiple names.", dependencyType));

            return dependencies.Select(s => s.Value).Single();
        }

        internal object GetDependency(Type dependencyType, string dependencyName)
        {
            var dependencies = Container.Where(w => w.Type == dependencyType && w.ConstructorPropertyName == dependencyName);

            if (dependencies.None())
                throw new InvalidOperationException(String.Format("Dependency of type {0} with propertyName {1} was not registered.", dependencyType, dependencyName));

            if (dependencies.Count() > 1)
                throw new InvalidOperationException(String.Format("Dependency of type {0} with propertyName {1} is registered multiple times.", dependencyType, dependencyName));

            return dependencies.Select(s => s.Value).Single();
        }

        internal object GetParentTypeDependency(Type extendingType)
        {
            var dependencies = Container.Where(w => extendingType.IsAssignableFrom(w.Type));

            if (dependencies.None())
                throw new InvalidOperationException(String.Format("No dependencies registered that are base type for {0}.", extendingType));

            if (dependencies.Count() > 1)
                throw new InvalidOperationException(String.Format("Multiple dependencies registered that are base type for {0}.", extendingType));

            return dependencies.Select(s => s.Value).Single();
        }

        internal object GetParentTypeDependency(Type extendingType, string dependencyName)
        {
            var dependencies = Container.Where(w => extendingType.IsAssignableFrom(w.Type) && w.ConstructorPropertyName == dependencyName);

            if (dependencies.None())
                throw new InvalidOperationException(String.Format("No dependencies registered under parameter name {1}, that are base type for {0} .", extendingType, dependencyName));

            if (dependencies.Count() > 1)
                throw new InvalidOperationException(String.Format("Multiple dependencies registered under parameter name {1}, that are base type for {0} .", extendingType, dependencyName));

            return dependencies.Select(s => s.Value).Single();
        }



        internal bool ContainsDependencyImplementation(Type implementsType)
        {
            return Container.Any(w => implementsType.IsAssignableFrom(w.Type));
        }

        internal bool ContainsDependencyImplementation(Type implementsType, string dependencyName)
        {
            return Container.Any(w => implementsType.IsAssignableFrom(w.Type) && w.ConstructorPropertyName == dependencyName);
        }



        internal void Clear()
        {
            Container.Clear();
        }


        public IEnumerator<Dependency> GetEnumerator()
        {
            return Container.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Container.GetEnumerator();
        }
    }
}