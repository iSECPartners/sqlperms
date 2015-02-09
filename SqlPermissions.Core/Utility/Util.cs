// Copyright (c) 2011-2015 iSEC Partners
// Author: Peter Oehlert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.


using System;

namespace SqlPermissions.Core.Utility
{
    /// <summary>Ughh, I hate utility classes. Need to rename this like EnumEx or something.</summary>
    public static class Util
    {
        public static T ParseEnum<T>(String enumString)
            where T : struct
        {
            const Boolean ShouldIgnoreCase = false;
            return ParseEnum<T>(enumString, ShouldIgnoreCase);
        }

        public static T ParseEnum<T>(String enumString, bool shouldIgnoreCase)
            where T : struct
        {
            return (T)Enum.Parse(typeof(T), enumString, shouldIgnoreCase);
        }

        public static bool TryParseEnum<T>(String enumString, out T value)
            where T : struct
        {
            const Boolean ShouldIgnoreCase = false;
            return TryParseEnum<T>(enumString, ShouldIgnoreCase, out value);
        }

        public static bool TryParseEnum<T>(String enumString, bool shouldIgnoreCase, out T value)
            where T : struct
        {
            return Enum.TryParse(enumString, shouldIgnoreCase, out value);
        }

    }
}
