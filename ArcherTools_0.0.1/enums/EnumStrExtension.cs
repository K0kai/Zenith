using System.Diagnostics;
using System.Reflection;

namespace ArcherTools_0._0._1.enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value) {
            Value = value;
        }

        public string Value { get; }
    }

    public static class EnumStrExtension
    {
        public static string StringValue<T>(this T value) where T : Enum
        {
            var fieldName = value.ToString();
            var field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);

            // Ensure field is found
            if (field == null)
            {
                Debug.WriteLine($"Field not found: {fieldName}");
                return fieldName;
            }

            var attribute = field.GetCustomAttribute<StringValueAttribute>();

            // Check if attribute is found
            if (attribute == null)
            {
                Debug.WriteLine($"No attribute found for: {fieldName}");
                return fieldName;
            }

            // Return the attribute value if found
            return attribute.Value;
        }
    }
    }
        

     
    

