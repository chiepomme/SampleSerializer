using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization;
using System.Diagnostics;

namespace SerializationPerformanceTest
{
    [TestClass]
    public class StringSerializerWithReflectionTest
    {
        [TestMethod]
        public void キャッシュ無しリフレクションによるシリアライズ()
        {
            var serializer = new StringSerializerWithReflection();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < Setting.PerformanceTestTrialCount; i++)
            {
                serializer.Serialize(Helper.TestObject);
            }
            stopwatch.Stop();

            Helper.PrintResult(stopwatch);
        }

        [TestMethod]
        public void キャッシュ無しリフレクションによるデシリアライズ()
        {
            var serializer = new StringSerializerWithReflection();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < Setting.PerformanceTestTrialCount; i++)
            {
                serializer.Deserialize<SimpleSerializableObject>(Helper.SerializedTestObject);
            }
            stopwatch.Stop();

            Helper.PrintResult(stopwatch);
        }
    }
}
