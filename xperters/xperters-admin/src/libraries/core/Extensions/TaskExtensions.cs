using System;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Xperters
{
    public static class TaskExtensions
    {
        public static bool Wait(this Task task, TimeSpan timeout, CancellationToken cancellationToken)
        {
            return task.Wait((int)Math.Round(timeout.TotalMilliseconds), cancellationToken);
        }
    }
}