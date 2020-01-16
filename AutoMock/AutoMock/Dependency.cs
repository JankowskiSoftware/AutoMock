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

namespace AutoMock
{
    /// <summary>
    /// Describes object dependency.
    /// Dependencies are objects passed to a constructor.
    /// </summary>
    public class Dependency
    {
        /// <summary>
        /// Under this type dependency is registered.
        /// </summary>
        public Type Type { get; private set; }
        
        /// <summary>
        /// Instance of the dependency.
        /// </summary>
        public object Value { get; private set; }
        
        /// <summary>
        /// When constructor constains multiple dependencies of the same type then it is necessary to specify parameter name.
        /// </summary>
        public string ConstructorPropertyName { get; private set; }

        
        /// <summary>
        /// Create dependency.
        /// </summary>
        /// <param name="value">Instance of the dependency and type of dependency.</param>
        /// <param name="constructorPropertyName">Parameter name from constructor.</param>
        public Dependency(object value, string constructorPropertyName = null)
        {
            Type = value.GetType();
            Value = value;
            ConstructorPropertyName = constructorPropertyName;
        }

        /// <summary>
        /// Create dependency.
        /// </summary>
        /// <param name="type">Type under with dependency is registered.</param>
        /// <param name="value">Instance of the dependency and type of dependency.</param>
        /// <param name="constructorPropertyName">Parameter name from constructor.</param>
        public Dependency(Type type, object value, string constructorPropertyName = null)
        {
            Type = type;
            Value = value;
            ConstructorPropertyName = constructorPropertyName;
        }

        /// <summary>
        /// Object is equal when Type and ConstructorPropertyName are equal.
        /// </summary>
        protected bool Equals(Dependency other)
        {
            return string.Equals(ConstructorPropertyName, other.ConstructorPropertyName) && Equals(Type, other.Type);
        }

        /// <summary>
        /// Object is equal when Type and ConstructorPropertyName are equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Dependency) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ConstructorPropertyName != null ? ConstructorPropertyName.GetHashCode() : 0) * 397) ^ (Type != null ? Type.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return String.Format("Dependency(type[{0}], propertyName[{1}])", Type.FullName, ConstructorPropertyName);
        }
    }
}