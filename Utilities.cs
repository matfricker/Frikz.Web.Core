using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frikz.Web.Core
{
    public static class Utilities
    {
        public static object DBNullHandler(object instance)
        {
            if (instance != null)
                return instance;
            else
                return DBNull.Value;
        }

        public static object DBIntHandler(object instance)
        {
            if (instance != null)
                return instance;
            else
                return 0;
        }

        public static T ConvertEx<T>(this object valueToConvert, T defaultValue)
        {
            Type realType = typeof(T);

            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return defaultValue;
            if (valueToConvert is T)
                return (T)valueToConvert;

            try
            {
                if (typeof(T).IsGenericType && Nullable.GetUnderlyingType(typeof(T)) != null)
                    realType = Nullable.GetUnderlyingType(typeof(T));

                return (T)Convert.ChangeType(valueToConvert, realType);
            }
            catch
            {
                int iOut;
                /// Special case for boolean as can be represented 
                /// by numbers or text, and std bool parsing doesn't 
                /// convert string numbers.
                if (realType == typeof(bool) && valueToConvert is string && int.TryParse((string)valueToConvert, out iOut))
                    return ConvertEx<T>(iOut, defaultValue);
                else
                    return defaultValue;
            }
        }
    }
}
