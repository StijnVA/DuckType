using System;
using DuckType.Core.DuckType;

namespace DuckType.Core.Smart
{
    public static class SmartExtensions
    {
        public static T MakeSmart<T>(this T self) where T : class
        {
            return SmartObjectFactory.GenerateSmartObject(self);
        }
        
        public static SmartController<T> GetSmartController<T>(this T self) where T : class
        {
            if(!(self is ISmartObject<T> smartObject)) throw new SmartException("The SmartController can only be retrieved for a smart object.");

            return smartObject.SmartController;
        }
        

        public static T AsDuck<T>(this object self, Action<DuckTypeOptions> options = null) where T : class
        {
            return new DuckTypeFactory().CreateDuckedTypeObject<T>(self, options);
        }
    }
}