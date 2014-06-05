using System;
using System.IO;
using System.Web.UI;

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

        public static string GetLimitedWords(string str, int NumberOfWords)
        {
            string[] Words = str.Split(' ');
            string _return = string.Empty;

            if (Words.Length <= NumberOfWords)
            {
                _return = str;
            }
            else
            {
                for (int i = 0; i < NumberOfWords; i++)
                {
                    _return += Words.GetValue(i).ToString();
                    if (i != NumberOfWords - 1)
                    {
                        _return += " ";
                    }
                }
            }
            return _return.ToString();
        }

        public static string GenerateTextAreaText(string str)
        {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(str.Replace("\n\n", "</p><p>"));
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
