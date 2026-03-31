using System.Diagnostics;

namespace CollectionBenchmarkWebApi.Services
{
    //класс для подсчёта времени и памяти работы коллекций на добавлении, поиске по количеству элементов
    public static class BenchmarkService
    {
        public static double GetAddTime(ICollection<int> collection, int elemCount)
        {
            var stopwatch = new Stopwatch();
            var rnd = new Random();
            stopwatch.Start();
            for (int i = 0; i< elemCount; i++)
            {
                collection.Add(rnd.Next(100));
            }
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalSeconds;
        }

        public static double GetSearchTime(ICollection<int> collection, int elemCount)
        {
            var stopwatch = new Stopwatch();
            var rnd = new Random();

            //добавление элементов
            for (int i = 0; i < elemCount; i++)
            {
                collection.Add(rnd.Next(100));
            }

            int findElem = rnd.Next(100);

            stopwatch.Start();
            collection.Contains(findElem);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalSeconds;
        }

        public static double GetCollectionMemory(ICollection<int> collection, int elemCount)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryBefore = GC.GetTotalMemory(true);

            for (int i = 0; i < elemCount; i++)
            {
                collection.Add(i);
            }

            long memoryAfter = GC.GetTotalMemory(true);

            return (memoryAfter - memoryBefore)/1024.0;
        }
    }
}
