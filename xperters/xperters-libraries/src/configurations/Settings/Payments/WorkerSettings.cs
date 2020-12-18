// ReSharper disable InconsistentNaming
namespace xperters.configurations.Settings.Payments
{
    public class WorkerSettings
    {
        public byte NumberOfWorkers { get; set; }
        public short ThreadSleepMilliseconds { get; set; }
        public int TestingMRPItemsCount { get; set; }
        public int TestingMSRPItemsCount { get; set; }
    }
}