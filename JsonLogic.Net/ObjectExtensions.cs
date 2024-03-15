using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JsonLogic.Net
{

    public static class ObjectExtensions
    {
        public static bool IsEnumerable(this object d)
        {
            return d != null && (d.GetType().IsArray || (d as IEnumerable<object>) != null);
        }

        public static IEnumerable<object> MakeEnumerable(this object value)
        {
            if (value is Array valueArray)
            {
                return valueArray.Cast<object>();
            }

            if (value is IEnumerable<object> valueEnumerable)
            {
                return valueEnumerable;
            }

            throw new ArgumentException("Argument is not enumerable");
        }

        public static bool EqualTo(this object value, object other)
        {
            if (value is string || other is string)
            {
                return Convert.ToString(value).Equals(Convert.ToString(other));
            }

            if ((value.IsNumeric() || value is bool) && (other.IsNumeric() || other is bool))
            {
                return Convert.ToDouble(value).Equals(Convert.ToDouble(other));
            }

            // special handling for nulls to avoid NullReferenceException
            if (value == null)
            {
                return other == null;
            }

            return value.Equals(other);
        }


        /// <summary>
        /// Equivalent to JavaScript "===" comparer.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool StrictEqualTo(this object value, object other)
        {
            return value == null || other == null ? value == other : value.Equals(other);
        }


        public static bool IsNumeric(this object value)
        {
            return value is byte || value is sbyte || value is short || value is ushort || value is int  || value is uint || value is long || value is decimal || value is float || value is double;
        }

        public static bool IsTruthy(this object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool valueBool)
            {
                return valueBool;
            }

            if (value.IsNumeric())
            {
                return Convert.ToDouble(value) != 0;
            }

            if (value.IsEnumerable())
            {
                return value.MakeEnumerable().Any();
            }

            if (value is string valueString)
            {
                return valueString.Length > 0;
            }

            return true;
        }
    }
}
