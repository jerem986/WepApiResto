using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.AutoMapper.Attributes
{
    public class MapPropertyAttribute : Attribute
    {
        public string Name { get; private set; }
        public MapPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}
