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
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;

namespace SqlPermissions.Core.Utility
{
    /// <summary>Set of extensions to the data tier (System.Data etc) to support easier access and provide
    /// helper methods for better integration.
    /// </summary>
    public static class DataExtensions
    {
        #region IDataRecord

        /// <summary>Gets the value2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Object GetNullableValue(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? null
                    : record.GetValue(index);
        }

        /// <summary>Gets the boolean2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Boolean? GetNullableBoolean(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Boolean?)null
                    : record.GetBoolean(index);
        }

        /// <summary>Gets the byte2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Byte? GetNullableByte(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Byte?)null
                    : record.GetByte(index);
        }

        /// <summary>Gets the char2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Char? GetNullableChar(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Char?)null
                    : record.GetChar(index);
        }

        /// <summary>Gets the guid2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Guid? GetNullableGuid(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Guid?)null
                    : record.GetGuid(index);
        }

        /// <summary>Gets the int162.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Int16? GetNullableInt16(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Int16?)null
                    : record.GetInt16(index);
        }

        /// <summary>Gets the int322.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Int32? GetNullableInt32(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Int32?)null
                    : record.GetInt32(index);
        }

        /// <summary>Gets the int642.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Int64? GetNullableInt64(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Int64?)null
                    : record.GetInt64(index);
        }

        /// <summary>Gets the float2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Single? GetNullableFloat(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Single?)null
                    : record.GetFloat(index);
        }

        /// <summary>Gets the double2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Double? GetNullableDouble(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Double?)null
                    : record.GetDouble(index);
        }

        /// <summary>Gets the string2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static String GetNullableString(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? null
                    : record.GetString(index);
        }

        /// <summary>Gets the decimal2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static Decimal? GetNullableDecimal(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (Decimal?)null
                    : record.GetDecimal(index);
        }

        /// <summary>Gets the date time2.</summary>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static DateTime? GetNullableDateTime(this IDataRecord record, int index)
        {
            Contract.Requires(null != record, "The record parameter must be valid.");

            return
                record.IsDBNull(index)
                    ? (DateTime?)null
                    : record.GetDateTime(index);
        }

		/// <summary>Tries to get the ordinal.</summary>
		/// <param name="record">The record to get the ordinal from.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="ordinal">The ordinal.</param>
		/// <returns>True on success, false otherwise.</returns>
		public static bool TryGetOrdinal(this IDataRecord record, string fieldName, out int? ordinal)
		{
			Contract.Requires(null != record);
			Contract.Requires(!string.IsNullOrEmpty(fieldName));

			// default value
			ordinal = null;
			try
			{
				ordinal = record.GetOrdinal(fieldName);
				return true;
			}
			catch (IndexOutOfRangeException) { /* Eat it */ }

			return false;
		}

        #endregion

        #region DbType

        /// <summary>Store a type map to convert db types to types</summary>
        private static readonly Dictionary<DbType, Type> DbTypeMap = new Dictionary<DbType, Type>
        { 
            // strings
            { DbType.AnsiString, typeof(String) },
            { DbType.AnsiStringFixedLength, typeof(String) },
            { DbType.String, typeof(String) },
            { DbType.StringFixedLength, typeof(String) },
            { DbType.Xml, typeof(String) },

            // binary
            { DbType.Binary, typeof(Byte[]) },

            // time
            { DbType.Date, typeof(DateTime) },
            { DbType.DateTime, typeof(DateTime) },
            { DbType.DateTime2, typeof(DateTime) },
            { DbType.Time, typeof(DateTime) },
            { DbType.DateTimeOffset, typeof(DateTimeOffset) },

            // misc
            { DbType.Boolean, typeof(Boolean) },
            { DbType.Currency, typeof(Decimal) },
            { DbType.Decimal, typeof(Decimal) },
            { DbType.VarNumeric, typeof(Decimal) },
            { DbType.Guid, typeof(Guid) },
            { DbType.Object, typeof(Object) },

            // integers
            { DbType.Byte, typeof(Byte) },
            { DbType.Int16, typeof(Int16) },
            { DbType.Int32, typeof(Int32) },
            { DbType.Int64, typeof(Int64) },
            { DbType.SByte, typeof(SByte) },
            { DbType.UInt16, typeof(UInt16) },
            { DbType.UInt32, typeof(UInt32) },
            { DbType.UInt64, typeof(UInt64) },

            // floating point numbers
            { DbType.Single, typeof(Single) },
            { DbType.Double, typeof(Double) },
        };

        /// <summary>Convert the DbType to the BCL type.</summary>
        /// <param name="dbType">The type from the db.</param>
        /// <returns>The BCL or System Type.</returns>
        public static Type ToType(this DbType dbType)
        {
            Type t;
            DbTypeMap.TryGetValue(dbType, out t);
            return t;
        }

        #endregion
    }
}
