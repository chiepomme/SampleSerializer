using System;
using System.Reflection;
using System.Text;

namespace Serialization
{
    public class StringSerializerWithReflection
    {
        public string Serialize<T>(T serializableObject) where T : new()
        {
            var stringBuilder = new StringBuilder();
            var fields = typeof(T).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                stringBuilder.AppendLine(field.Name + "\t" + field.GetValue(serializableObject));
            }
            return stringBuilder.ToString().Replace("\r\n", "\n");
        }

        public T Deserialize<T>(string serializedString) where T : new()
        {
            var resultObj = new T();
            var fields = typeof(T).GetFields(BindingFlags.SetField | BindingFlags.Public | BindingFlags.Instance);

            foreach (var line in serializedString.Split('\n'))
            {
                if (string.IsNullOrEmpty(line)) continue;
                var nameAndValue = line.Split('\t');
                var name = nameAndValue[0];
                var rawValue = nameAndValue[1];
                var field = Array.Find(fields, (e) => e.Name == name);

                SetValueToField(resultObj, field, rawValue);
            }

            return resultObj;
        }

        void SetValueToField<T>(T resultObj, FieldInfo field, string rawValue)
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(resultObj, int.Parse(rawValue));
            }
            else if (field.FieldType == typeof(string))
            {
                field.SetValue(resultObj, rawValue);
            }
        }
    }
}
