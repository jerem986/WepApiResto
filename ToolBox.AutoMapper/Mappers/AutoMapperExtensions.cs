using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToolBox.AutoMapper.Attributes;

namespace ToolBox.AutoMapper.Mappers
{
    public static class AutoMapperExtensions
    {
        public static TResult MapTo<TResult>(this object from, Action<TResult> process = null, params object[] parameters)
        {
            ConstructorInfo ctor = typeof(TResult)
                .GetConstructor(parameters.Select(p => p.GetType()).ToArray());
            if (ctor is null) throw new ArgumentException();
            TResult result = (TResult)ctor.Invoke(parameters);
            return from.MapToInstance(result, process);
        }

        public static TResult MapToInstance<TResult>(this object from, TResult result, Action<TResult> process = null)
        {
            IEnumerable<PropertyInfo> properties = typeof(TResult).GetProperties();
            foreach (PropertyInfo property in properties
                .Where(p => p.CanWrite && !p.IsDefined(typeof(MapIgnoreAttribute)))
            )
            {
                MapPropertyAttribute mapPropertyAttribute = property.GetCustomAttribute<MapPropertyAttribute>();
                string propName = property.GetCustomAttribute<MapPropertyAttribute>()?.Name ?? property.Name;
                PropertyInfo fromProp = from.GetType().GetProperty(propName);
                if (!(fromProp is null) && fromProp.CanRead)
                {
                    try
                    {
                        property.SetValue(result, fromProp.GetValue(from));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            process?.Invoke(result);
            return result;
        }

        public static IEnumerable<TResult> MapToList<TResult>(this IEnumerable<object> fromList, Action<TResult> process = null)
        {
            return fromList.Select(from => from.MapTo<TResult>(process));
        }
    }
}
