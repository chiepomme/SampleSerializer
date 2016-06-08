// このファイルは StaticStringSerializerGenerator によって生成されました
using System.Text;

namespace Serialization
{
    public partial class SimpleSerializableObject
    {
        public string Serialize()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Int1\t" + Int1);
            sb.AppendLine("Int2\t" + Int2);
            sb.AppendLine("Int3\t" + Int3);
            sb.AppendLine("String1\t" + String1);
            sb.AppendLine("String2\t" + String2);
            sb.AppendLine("String3\t" + String3);
            return sb.ToString().Replace("\r\n", "\n");
        }

        public SimpleSerializableObject(string serializedString)
        {
            foreach (var line in serializedString.Split('\n'))
            {
                if (string.IsNullOrEmpty(line)) continue;
                var nameAndValue = line.Split('\t');
                var name = nameAndValue[0];
                var rawValue = nameAndValue[1];

                switch (name)
                {
                    case "Int1": Int1 = int.Parse(rawValue); break;
                    case "Int2": Int2 = int.Parse(rawValue); break;
                    case "Int3": Int3 = int.Parse(rawValue); break;
                    case "String1": String1 = rawValue; break;
                    case "String2": String2 = rawValue; break;
                    case "String3": String3 = rawValue; break;
                }
            }
        }
    }
}
