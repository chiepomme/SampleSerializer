using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace SerializerGenerator
{
    class StaticStringSerializerGenerator
    {
        public void Generate(Type type)
        {
            File.WriteAllText(type.Name + ".Serializer.cs", Stringify(type));
        }

        public string Stringify(Type type)
        {
            var fields = type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance);

            // 本当はハードコードじゃなくて何かしらのテンプレートを使ってね！
            var sb = new StringBuilder();
            sb.AppendLine(@"// このファイルは StaticStringSerializerGenerator によって生成されました");
            sb.AppendLine(@"using System.Text;");
            sb.AppendLine();
            sb.AppendLine(@"namespace " + type.Namespace);
            sb.AppendLine(@"{");
            sb.AppendLine(@"    public partial class " + type.Name);
            sb.AppendLine(@"    {");
            sb.AppendLine(@"        public string Serialize()");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"            var sb = new StringBuilder();");
            foreach (var field in fields)
            {
                sb.AppendFormat(@"            sb.AppendLine(""{0}\t"" + {0});", field.Name);
                sb.AppendLine();
            }
            sb.AppendLine(@"            return sb.ToString().Replace(""\r\n"", ""\n"");");
            sb.AppendLine(@"        }");
            sb.AppendLine();
            sb.AppendLine(@"        public " + type.Name + "(string serializedString)");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"            foreach (var line in serializedString.Split('\n'))");
            sb.AppendLine(@"            {");
            sb.AppendLine(@"                if (string.IsNullOrEmpty(line)) continue;");
            sb.AppendLine(@"                var nameAndValue = line.Split('\t');");
            sb.AppendLine(@"                var name = nameAndValue[0];");
            sb.AppendLine(@"                var rawValue = nameAndValue[1];");
            sb.AppendLine();
            sb.AppendLine(@"                switch (name)");
            sb.AppendLine(@"                {");

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(int))
                {
                    sb.AppendFormat(@"                    case ""{0}"": {0} = int.Parse(rawValue); break;", field.Name);
                }
                else if (field.FieldType == typeof(string))
                {
                    sb.AppendFormat(@"                    case ""{0}"": {0} = rawValue; break;", field.Name);
                }
                sb.AppendLine();
            }

            sb.AppendLine(@"                }");
            sb.AppendLine(@"            }");
            sb.AppendLine(@"        }");
            sb.AppendLine(@"    }");
            sb.AppendLine(@"}");

            return sb.ToString();
        }
    }
}
