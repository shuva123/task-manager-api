using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Unit_Test;
namespace NBench_Test
{

    public class NBenchTestClass
    {
        private Counter counter;
        private IBenchmarkTrace trace;
        UnitTest1 tc = new UnitTest1();
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            counter = context.GetCounter("MyCounter");
            trace = context.Trace;

        }
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)][CounterMeasurement("MyCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)][TimingMeasurement()]
        public void BenchMark_Method(BenchmarkContext context)
        {
            tc.GetAllTasks();
            tc.GetSupplier_ShouldReturnItemWithSameID();
            tc.PostTask_ShouldReturnSameItem();
            tc.PutSupplier_ShouldFail_WhenDifferentID();
            tc.DeleteTask_ShouldReturnOK();
            counter.Increment();
        }
    }
   
  
}
