using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TestValues
{
    static class TestValueHelper
    {
        private static readonly Dictionary<Type, Func<string, object>> @switch;
        static TestValueHelper()
        {
            @switch = new Dictionary<Type, Func<string, object>> {
                { typeof(int), (btv) => {return btv.GetHashCode(); } },
                { typeof(int?), (btv) => {return btv.GetHashCode(); } },
                { typeof(string), (btv) => { return "Test" + btv; } },
                { typeof(DateTime), (btv) => { return new DateTime((long) Math.Abs(btv.GetHashCode()) * 1000000000); } },
                { typeof(DateTime?), (btv) => { return new DateTime((long) Math.Abs(btv.GetHashCode()) * 1000000000); } },
            };
        }
        public static T GetTestValue<T>(Expression<Func<T>> property, string additionalValue = "")
        {
            var memberExpression = property.Body as MemberExpression;
            var propertyName = memberExpression.Member.Name;

            return GetTestValue<T>(propertyName, additionalValue);
        }

        public static T GetTestValue<T>(string propertyName, string additionalValue = "")
        {
            var baseTestValue = propertyName + additionalValue;

            if (!@switch.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"The type of {typeof(T).Name} is not supported");
            }

            return (T)@switch[typeof(T)](baseTestValue);
        }
    }
}
