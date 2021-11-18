using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ToolBox.Utils.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retreive the value of EnumMemberAttribute or call ToString() if EnumMemberAttribute is not defined
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string ToMemberString(this Enum enumValue)
        {
            Type t = enumValue.GetType();
            MemberInfo member = t.GetMember(enumValue.ToString()).First();
            EnumMemberAttribute attribute = member.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                .OfType<EnumMemberAttribute>().FirstOrDefault();
            return attribute?.Value ?? enumValue.ToString();
        }
    }
}
