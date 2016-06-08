using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Serialization
{
    public class StringSerializerWithCachedReflection
    {
        readonly Dictionary<Type, Dictionary<string, FieldInfo>> CachedFields = new Dictionary<Type, Dictionary<string, FieldInfo>>();

        public string Serialize<T>(T serializableObject) where T : new()
        {
            var type = typeof(T);
            CreateCacheIfNotCached(type);

            var stringBuilder = new StringBuilder();
            foreach (var field in CachedFields[type].Values)
            {
                stringBuilder.AppendLine(field.Name + "\t" + field.GetValue(serializableObject));
            }
            return stringBuilder.ToString().Replace("\r\n", "\n");
        }

        public T Deserialize<T>(string serializedString) where T : new()
        {
            var type = typeof(T);
            CreateCacheIfNotCached(type);

            var resultObj = new T();
            var fieldMapByName = CachedFields[type];

            foreach (var line in serializedString.Split('\n'))
            {
                if (string.IsNullOrEmpty(line)) continue;
                var nameAndValue = line.Split('\t');
                var name = nameAndValue[0];
                var rawValue = nameAndValue[1];
                var field = fieldMapByName[name];

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

        void CreateCacheIfNotCached(Type type)
        {
            if (!CachedFields.ContainsKey(type))
            {
                CachedFields[type] = type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance)
                                         .ToDictionary(f => f.Name, f => f);
            }
        }
    }
}
