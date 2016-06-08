using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization;
using System.Diagnostics;

namespace SerializationPerformanceTest
{
    [TestClass]
    public class StringSerializerWithCachedReflectionTest
    {
        [TestMethod]
        public void キャッシュ付きのリフレクションによるシリアライズ()
        {
            var serializer = new StringSerializerWithCachedReflection();

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
        public void キャッシュ付きのリフレクションによるデシリアライズ()
        {
            var serializer = new StringSerializerWithCachedReflection();

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
