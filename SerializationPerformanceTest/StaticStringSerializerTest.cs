using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization;
using System.Diagnostics;

namespace SerializationPerformanceTest
{
    [TestClass]
    public class StaticStringSerializerTest
    {
        [TestMethod]
        public void 静的なシリアライザを使用したシリアライズ()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < Setting.PerformanceTestTrialCount; i++)
            {
                Helper.TestObject.Serialize();
            }
            stopwatch.Stop();

            Helper.PrintResult(stopwatch);
        }

        [TestMethod]
        public void 静的なシリアライザを使用したデシリアライズ()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < Setting.PerformanceTestTrialCount; i++)
            {
                new SimpleSerializableObject(Helper.SerializedTestObject);
            }
            stopwatch.Stop();

            Helper.PrintResult(stopwatch);
        }
    }
}
