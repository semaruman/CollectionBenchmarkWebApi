namespace CollectionBenchmarkWebApi.Services
{
    public interface ICollesctionsService
    {
        public List<string> Types { get; set; }

        public int ElementsCount { get; set; }
    }
}
