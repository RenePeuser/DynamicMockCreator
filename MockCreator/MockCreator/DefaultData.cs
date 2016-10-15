using System;
using System.Collections.Generic;
using Extensions;

namespace MockCreator
{
    public class DefaultData
    {
        private readonly Dictionary<Type, object> _defaultData;

        public DefaultData(params object[] defaultData)
        {
            _defaultData = new Dictionary<Type, object>();

            // ToDo: Dictionary.AddRange Extensions => AddRange(this Dictionary dictionary, Func<TKey> keySelector, Func<TValue> valueSelector)
            defaultData.ForEach(item => _defaultData.Add(item.GetType(), item));
        }

        public void Add<T>(T item)
        {
            var expectedType = typeof(T);
            if (!_defaultData.ContainsKey(expectedType))
            {
                _defaultData.Add(expectedType, item);
            }
        }

        public object GetDefaultValue(Type type)
        {
            return _defaultData.GetValueOrDefault(type);
        }
    }
}
