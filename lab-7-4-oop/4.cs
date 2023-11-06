using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7_4_oop
{
     //1
    public class Calculator<T>
    {
        public delegate T OperationDelegate(T a, T b);

        public OperationDelegate Add { get; set; }
        public OperationDelegate Subtract { get; set; }
        public OperationDelegate Multiply { get; set; }
        public OperationDelegate Divide { get; set; }

        public T PerformOperation(OperationDelegate operation, T a, T b)
        {
            return operation(a, b);
        }
    }

     //2
    public class Repository<T>
    {
        private List<T> items;

        public Repository()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> Find(Criteria<T> criteria)
        {
            List<T> results = new List<T>();
            foreach (T item in items)
            {
                if (criteria(item))
                {
                    results.Add(item);
                }
            }
            return results;
        }
    }

    public delegate bool Criteria<T>(T item);

     //3
    public class FunctionCache<TKey, TResult>
    {
        private Dictionary<TKey, CacheItem<TResult>> cache;
        private TimeSpan expirationTime;

        public FunctionCache(TimeSpan expirationTime)
        {
            this.expirationTime = expirationTime;
            cache = new Dictionary<TKey, CacheItem<TResult>>();
        }

        public TResult ExecuteFunction(Func<TKey, TResult> function, TKey key)
        {
            if (cache.ContainsKey(key))
            {
                var cacheItem = cache[key];
                if ((DateTime.Now - cacheItem.TimeStamp) < expirationTime)
                {
                    return cacheItem.Result;
                }
            }

            TResult result = function(key);
            cache[key] = new CacheItem<TResult>(result, DateTime.Now);
            return result;
        }

        private class CacheItem<T>
        {
            public T Result { get; }
            public DateTime TimeStamp { get; }

            public CacheItem(T result, DateTime timeStamp)
            {
                Result = result;
                TimeStamp = timeStamp;
            }
        }
    }

     //4
    public class TaskScheduler<TTask, TPriority>
    {
        private SortedDictionary<TPriority, Queue<TTask>> taskQueue;
        private Func<TTask, TPriority> prioritySelector;
        private Action<TTask> taskExecutor;

        public TaskScheduler(Func<TTask, TPriority> prioritySelector, Action<TTask> taskExecutor)
        {
            this.prioritySelector = prioritySelector;
            this.taskExecutor = taskExecutor;
            taskQueue = new SortedDictionary<TPriority, Queue<TTask>>();
        }

        public void AddTask(TTask task)
        {
            TPriority priority = prioritySelector(task);
            if (!taskQueue.ContainsKey(priority))
            {
                taskQueue[priority] = new Queue<TTask>();
            }
            taskQueue[priority].Enqueue(task);
        }

        public void ExecuteNext()
        {
            var highestPriority = taskQueue.Keys.GetEnumerator().MoveNext() ? taskQueue.Keys.GetEnumerator().Current : default(TPriority);
            if (highestPriority != null && taskQueue[highestPriority].Count > 0)
            {
                TTask task = taskQueue[highestPriority].Dequeue();
                taskExecutor(task);
            }
            else
            {
                Console.WriteLine("No tasks to execute.");
            }
        }
    }
}
