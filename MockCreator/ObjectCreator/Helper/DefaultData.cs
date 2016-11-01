using System;
using System.Collections.Generic;
using Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public class DefaultData : IDefaultData
    {
        private readonly Dictionary<Type, object> _defaultData;

        public DefaultData(params object[] defaultData)
        {
            _defaultData = new Dictionary<Type, object>();

            // ToDo: Dictionary.AddRange Extensions => AddRange(this Dictionary dictionary, Func<TKey> keySelector, Func<TValue> valueSelector)
            defaultData.ForEach(item => _defaultData.Add(item.GetType(), item));
        }

        public object GetDefaultValue(Type type)
        {
            return _defaultData.GetValueOrDefault(type);
        }
    }
}