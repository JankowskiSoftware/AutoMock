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

using System.Collections.Generic;
using System.Linq;

namespace AutoMock.Internals
{
    internal static class NonLINQExtension
    {
        public static bool None<T>(this IEnumerable<T> enumeration)
        {
            return !enumeration.Any();
        }
    }
}
