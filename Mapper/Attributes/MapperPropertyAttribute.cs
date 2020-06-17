using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper.Attributes
{
    public class MapperPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public MapperPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
