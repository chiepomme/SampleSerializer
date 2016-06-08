using Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerializationTest
{
    [TestClass]
    public class StringSerializerWithCachedReflectionTest
    {
        [TestMethod]
        public void キャッシュ付きリフレクションで正しくシリアライズできる()
        {
            var serializableObject = new SimpleSerializableObject()
            {
                Int1 = 1,
                Int2 = 2,
                Int3 = 3,
                String1 = "abc",
                String2 = "def",
                String3 = "ghi",
            };

            var serializer = new StringSerializerWithCachedReflection();

            Assert.AreEqual(
                "Int1\t1\n" + "Int2\t2\n" + "Int3\t3\n" + "String1\tabc\n" + "String2\tdef\n" + "String3\tghi\n",
                serializer.Serialize(serializableObject));
        }

        [TestMethod]
        public void キャッシュ付きリフレクションで正しくデシリアライズできる()
        {
            var serializableObject = new SimpleSerializableObject()
            {
                Int1 = 1,
                Int2 = 2,
                Int3 = 3,
                String1 = "abc",
                String2 = "def",
                String3 = "ghi",
            };

            var serializedString = serializableObject.Serialize();
            var deserializedObject = new StringSerializerWithCachedReflection().Deserialize<SimpleSerializableObject>(serializedString);
            Assert.AreEqual(1, deserializedObject.Int1);
            Assert.AreEqual(2, deserializedObject.Int2);
            Assert.AreEqual(3, deserializedObject.Int3);
            Assert.AreEqual("abc", deserializedObject.String1);
            Assert.AreEqual("def", deserializedObject.String2);
            Assert.AreEqual("ghi", deserializedObject.String3);
        }
    }
}
