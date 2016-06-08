using Serialization;
using System;
using System.Diagnostics;

namespace SerializationPerformanceTest
{
    public static class Helper
    {
        public static readonly SimpleSerializableObject TestObject = new SimpleSerializableObject()
        {
            Int1 = 1,
            Int2 = 2,
            Int3 = 3,
            String1 = "abcde",
            String2 = "fgehij",
            String3 = "klmnop",
        };

        public static readonly string SerializedTestObject = TestObject.Serialize();

        public static void PrintResult(Stopwatch stoppedStopwatch)
        {
            Console.WriteLine(stoppedStopwatch.Elapsed.TotalMilliseconds + "ミリ秒");
        }
    }
}
