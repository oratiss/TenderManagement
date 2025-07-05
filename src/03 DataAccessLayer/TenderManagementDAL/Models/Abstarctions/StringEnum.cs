using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenderManagementDAL.Models.Abstarctions
{
    public abstract record StringEnum<T> where T : StringEnum<T>
    {
        public string Value { get; }

        protected StringEnum(string value) => Value = value;

        public override string ToString() => Value;

        public static IEnumerable<T> List()
        {
            return typeof(T)
                .GetFields(System.Reflection.BindingFlags.Public |
                           System.Reflection.BindingFlags.Static |
                           System.Reflection.BindingFlags.DeclaredOnly)
                .Where(f => f.FieldType == typeof(T))
                .Select(f => (T)f.GetValue(null)!);
        }

        public static T FromValue(string value)
        {
            var match = List().FirstOrDefault(e => e.Value == value);
            return match ?? throw new ArgumentException($"Invalid value '{value}' for {typeof(T).Name}");
        }
    }
}
