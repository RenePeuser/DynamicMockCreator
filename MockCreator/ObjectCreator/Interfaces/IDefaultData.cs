using System;

namespace ObjectCreator.Interfaces
{
    public interface IDefaultData
    {
        object GetDefaultValue(Type type);
    }
}
