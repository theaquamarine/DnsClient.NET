﻿using System.Threading;
namespace System.Threading.Tasks
{
    internal static class TaskExtensions
    {
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout, CancellationToken parentToken)
        {
            var cts = new CancellationTokenSource();            
            var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, parentToken);
            var linkedToken = linkedSource.Token;
            int millis = timeout.TotalMilliseconds > int.MaxValue ? int.MaxValue : (int)timeout.TotalMilliseconds;

            if (task == await Task.WhenAny(task, Task.Delay(millis, linkedToken)).ConfigureAwait(false))
            {
                linkedSource.Cancel();
                await task.ConfigureAwait(false);
            }
            else
            {
                if (parentToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                throw new TimeoutException();
            }
        }

        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout, CancellationToken parentToken)
        {
            var cts = new CancellationTokenSource();            
            var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, parentToken);
            var linkedToken = linkedSource.Token;
            int millis = timeout.TotalMilliseconds > int.MaxValue ? int.MaxValue : (int)timeout.TotalMilliseconds;

            if (task == await Task.WhenAny(task, Task.Delay(millis, linkedToken)).ConfigureAwait(false))
            {
                linkedSource.Cancel();
                return await task.ConfigureAwait(false);
            }
            else
            {
                if (parentToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                throw new TimeoutException();
            }
        }
    }
}