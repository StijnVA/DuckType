using System;
using DuckType.Core.DuckType;

namespace DuckType.Core.Smart
{
    public interface IDuckTypeFactory
    {
        T CreateDuckedTypeObject<T>(object original, Action<DuckTypeOptions> options = null) where T : class;
    }
}