using Microsoft.AspNetCore.Mvc;

namespace CollectionBenchmarkWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenchmarkController : ControllerBase
    {
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
