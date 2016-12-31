using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Extensions
{
    public static class PrivateObjectExtensions
    {
        public static T GetField<T>(this PrivateObject privateObject, string fieldName) where T : class
        {
            if (privateObject == null)
            {
                throw new ArgumentNullException(nameof(privateObject));
            }

            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }
            return privateObject.GetField(fieldName).As<T>();
        }
    }
}