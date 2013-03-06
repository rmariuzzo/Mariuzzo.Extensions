using System;
using System.ComponentModel;
using System.Reflection;

namespace Mariuzzo.Extensions
{
    /// <summary>
    /// The <code>EnumExtensions</code> class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the description annotated value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description annotated value.</returns>
        public static String GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }

        /// <summary>
        /// Try to parse a string to an enum.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum TryParse<TEnum>(this Enum e, String value, TEnum? defaultValue = null) where TEnum : struct
        {
            // As seen at: http://stackoverflow.com/a/12199994/439427
            TEnum tmp;
            if (!Enum.TryParse<TEnum>(value, true, out tmp))
            {
                tmp = defaultValue ?? new TEnum();
            }
            return tmp;
        }
    }
}
