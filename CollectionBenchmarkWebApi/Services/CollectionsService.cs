namespace CollectionBenchmarkWebApi.Services
{
    public class CollectionsService : ICollesctionsService
    {
        public List<string> Types { get; set; } = new List<string>();

        public int ElementsCount { get; set; }
    }
}
