using System.Collections.Generic;

namespace DioLive.Common.Extensions
{
    public static class QueueExtensions
    {
        public static IEnumerable<T> Dequeue<T>(this Queue<T> queue, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return queue.Dequeue();
            }
        }

        public static IEnumerable<T> DequeueIfAvailable<T>(this Queue<T> queue, int maxCount)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (queue.Count == 0)
                {
                    yield break;
                }

                yield return queue.Dequeue();
            }
        }
    }
}