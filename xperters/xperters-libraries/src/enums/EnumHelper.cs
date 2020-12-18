using System;
using System.ComponentModel;

namespace xperters.enums
{
    public static class EnumHelper
    {
        // Get the value of the description attribute if the   
        // enum has one, otherwise use the value.  
        public static string GetDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            return value.ToString();
        }

        public static string GetFreelancerDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                var attributes = (FreelancerDescription[])fi.GetCustomAttributes(typeof(FreelancerDescription), false);
                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }


        public static int GetEnumValue<TEnum>(this TEnum value)
        {
            int val = Convert.ToInt32(value);
            return val;
        }
        public static string GetEnumValueInString<TEnum>(this TEnum value)
        {
            int val = Convert.ToInt32(value);
            return val.ToString();
        }
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}