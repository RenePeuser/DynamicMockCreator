using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public class DefaultData : IDefaultData
    {
        private readonly Dictionary<Type, object> _defaultData;

        public DefaultData(params object[] defaultData)
            : this(defaultData.Select(item => new TypeToValue(item.GetType(), item)).ToArray())
        {
        }

        public DefaultData(params TypeToValue[] typeToValue)
        {
            _defaultData = typeToValue.ToDictionary(item => item.Type, item => item.Value);
        }

        public object GetDefaultValue(Type type)
        {
            return _defaultData.GetValueOrDefault(type);
        }
    }
}