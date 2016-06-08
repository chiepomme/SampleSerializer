using Serialization;

namespace SerializerGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new StaticStringSerializerGenerator();
            generator.Generate(typeof(SimpleSerializableObject));
        }
    }
}
