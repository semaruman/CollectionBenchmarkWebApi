using CollectionBenchmarkWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollectionBenchmarkWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenchmarkController : ControllerBase
    {
        private ICollesctionsService _collesctionsService;

        private readonly string[] _acceptableTypes = new string[]
        {
            "List", "HashSet", "LinkedList", "SortedSet"
        };

        public BenchmarkController(ICollesctionsService collesctionsService)
        {
            _collesctionsService = collesctionsService;
        }

        [HttpGet("types")]
        public IActionResult Types()
        {
            return Ok(new
            {
                Types = _acceptableTypes
            });
        }

        [HttpPost("addcollection")]
        public IActionResult AddCollection([FromQuery] string type)
        {
            string types = "";
            if (Request.Cookies.ContainsKey("types"))
            {
                types = Request.Cookies["types"];
                Response.Cookies.Delete("types");
            }

            //если коллекция такого типа существует и она ещё не добавлена 
            if (_acceptableTypes.Contains(type) && !types.Contains(type))
            {
                
                types += type + ";";
                Response.Cookies.Append("types", types);

                return Ok(new {message = $"Коллекция {type} добавлена успешно!"});
            }
            else if (!_acceptableTypes.Contains(type))
            {
                return BadRequest(new {Error = $"Коллекции {type} не существует в программе!"});
            }
            else
            {
                return BadRequest(new { Error = $"Коллекция {type} уже добавлена" });
            }

        }

        [HttpPost("count")]
        public IActionResult AddElementsCount([FromQuery] int count)
        {
            /*
            if (count > 10000000)
            {
                return BadRequest(new {Error = "Значение не должно превышать 10000000"});
            }
            */
            if (count  < 0)
            {
                return BadRequest(new { Error = "Значение не должно быть отрицательным" });
            }

            if (Request.Cookies.ContainsKey("count"))
            {
                Response.Cookies.Delete("count");
                Response.Cookies.Append("count", count.ToString());
            }
            else
            {
                Response.Cookies.Append("count", count.ToString());
            }

            return Ok(new { message = $"Теперь сравнение происходит по {count} элементам" });
        }

        [HttpGet("selectedtypes")]
        public IActionResult GetSelectedTypes()
        {
            string[] types = new string[0];
            if (Request.Cookies.ContainsKey("types"))
            {
                types = Request.Cookies["types"].Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            return Ok(new
            {
                Types = types
            });
        }

        [HttpGet("add")]
        public IActionResult AddBenchmarkTest()
        {
            int elementsCount = 0;
            if (Request.Cookies.ContainsKey("count"))
            {
                elementsCount = Convert.ToInt32(Request.Cookies["count"]);
            }

            string[] types = new string[0];
            if (Request.Cookies.ContainsKey("types"))
            {
                types = Request.Cookies["types"].Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            if (types.Length == 0)
            {
                return BadRequest(new {Error = "Коллекии не добавлены"});
            }
            else if (elementsCount == 0)
            {
                return BadRequest(new { Error = "Укажите количество элементов" });
            }

            Dictionary<string, double> results = new Dictionary<string, double>();
            foreach (string type in types)
            {
                if (type == _acceptableTypes[0])
                {
                    double seconds = BenchmarkService.GetAddTime(new List<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[1])
                {
                    double seconds = BenchmarkService.GetAddTime(new HashSet<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[2])
                {
                    double seconds = BenchmarkService.GetAddTime(new LinkedList<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[3])
                {
                    double seconds = BenchmarkService.GetAddTime(new SortedSet<int>(), elementsCount);
                    results[type] = seconds;
                }
            }

            return Ok(results);
        }

        [HttpGet("search")]
        public IActionResult SearchBenchmarkTest()
        {
            int elementsCount = 0;
            if (Request.Cookies.ContainsKey("count"))
            {
                elementsCount = Convert.ToInt32(Request.Cookies["count"]);
            }

            string[] types = new string[0];
            if (Request.Cookies.ContainsKey("types"))
            {
                types = Request.Cookies["types"].Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            if (types.Length == 0)
            {
                return BadRequest(new { Error = "Коллекии не добавлены" });
            }
            else if (elementsCount == 0)
            {
                return BadRequest(new { Error = "Укажите количество элементов" });
            }

            Dictionary<string, double> results = new Dictionary<string, double>();
            foreach (string type in types)
            {
                if (type == _acceptableTypes[0])
                {
                    double seconds = BenchmarkService.GetSearchTime(new List<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[1])
                {
                    double seconds = BenchmarkService.GetSearchTime(new HashSet<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[2])
                {
                    double seconds = BenchmarkService.GetSearchTime(new LinkedList<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[3])
                {
                    double seconds = BenchmarkService.GetSearchTime(new SortedSet<int>(), elementsCount);
                    results[type] = seconds;
                }
            }

            return Ok(results);
        }

        [HttpGet("memory")]
        public IActionResult MemoryBenchmarkTest()
        {
            int elementsCount = 0;
            if (Request.Cookies.ContainsKey("count"))
            {
                elementsCount = Convert.ToInt32(Request.Cookies["count"]);
            }

            string[] types = new string[0];
            if (Request.Cookies.ContainsKey("types"))
            {
                types = Request.Cookies["types"].Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            if (types.Length == 0)
            {
                return BadRequest(new { Error = "Коллекии не добавлены" });
            }
            else if (elementsCount == 0)
            {
                return BadRequest(new { Error = "Укажите количество элементов" });
            }

            Dictionary<string, double> results = new Dictionary<string, double>();
            foreach (string type in types)
            {
                if (type == _acceptableTypes[0])
                {
                    double mbytes = BenchmarkService.GetCollectionMemory(new List<int>(), elementsCount);
                    results[type] = mbytes;
                }
                else if (type == _acceptableTypes[1])
                {
                    double mbytes = BenchmarkService.GetCollectionMemory(new HashSet<int>(), elementsCount);
                    results[type] = mbytes;
                }
                else if (type == _acceptableTypes[2])
                {
                    double mbytes = BenchmarkService.GetCollectionMemory(new LinkedList<int>(), elementsCount);
                    results[type] = mbytes;
                }
                else if (type == _acceptableTypes[3])
                {
                    double mbytes = BenchmarkService.GetCollectionMemory(new SortedSet<int>(), elementsCount);
                    results[type] = mbytes;
                }
            }

            return Ok(results);
        }

        [HttpGet("remove")]
        public IActionResult RemoveBenchmarkTest()
        {
            int elementsCount = 0;
            if (Request.Cookies.ContainsKey("count"))
            {
                elementsCount = Convert.ToInt32(Request.Cookies["count"]);
            }

            string[] types = new string[0];
            if (Request.Cookies.ContainsKey("types"))
            {
                types = Request.Cookies["types"].Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            if (types.Length == 0)
            {
                return BadRequest(new { Error = "Коллекии не добавлены" });
            }
            else if (elementsCount == 0)
            {
                return BadRequest(new { Error = "Укажите количество элементов" });
            }

            Dictionary<string, double> results = new Dictionary<string, double>();
            foreach (string type in types)
            {
                if (type == _acceptableTypes[0])
                {
                    double seconds = BenchmarkService.GetRemoveTime(new List<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[1])
                {
                    double seconds = BenchmarkService.GetRemoveTime(new HashSet<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[2])
                {
                    double seconds = BenchmarkService.GetRemoveTime(new LinkedList<int>(), elementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[3])
                {
                    double seconds = BenchmarkService.GetRemoveTime(new SortedSet<int>(), elementsCount);
                    results[type] = seconds;
                }
            }

            return Ok(results);
        }
    }
}
