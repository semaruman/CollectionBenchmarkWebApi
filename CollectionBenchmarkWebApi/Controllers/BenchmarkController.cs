using CollectionBenchmarkWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollectionBenchmarkWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenchmarkController : ControllerBase
    {
        private ICollesctionsService _collesctionsService;

        public BenchmarkController(ICollesctionsService collesctionsService)
        {
            _collesctionsService = collesctionsService;
        }

        [HttpGet("types")]
        public IActionResult Types()
        {
            return Ok(new
            {
                Types = new[]
                {
                    "List", "Dictionary", "HashSet", "LinkedList", "SortedSet"
                }
            });
        }
    }
}
