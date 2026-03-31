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
            //если коллекция такого типа существует и она ещё не добавлена 
            if (_acceptableTypes.Contains(type) && !_collesctionsService.Types.Contains(type))
            {
                _collesctionsService.Types.Add(type);
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
            if (count > 10000000)
            {
                return BadRequest(new {Error = "Значение не должно превышать 10000000"});
            }
            else if (count  < 0)
            {
                return BadRequest(new { Error = "Значение не должно быть отрицательным" });
            }
            else
            {
                _collesctionsService.ElementsCount = count;
                return Ok(new {message = $"Теперь сравнение происходит по {count} элементам"});
            }
        }

        [HttpGet("selectedtypes")]
        public IActionResult GetSelectedTypes()
        {
            return Ok(new
            {
                Types = _collesctionsService.Types
            });
        }

        [HttpGet("add")]
        public IActionResult AddBenchmarkTest()
        {
            if (_collesctionsService.Types.Count == 0)
            {
                return BadRequest(new {Error = "Коллекии не добавлены"});
            }
            else if (_collesctionsService.ElementsCount == 0)
            {
                return BadRequest(new { Error = "Укажите количество элементов" });
            }

            Dictionary<string, double> results = new Dictionary<string, double>();
            foreach (string type in _collesctionsService.Types)
            {
                if (type == _acceptableTypes[0])
                {
                    double seconds = BenchmarkService.GetAddTime(new List<int>(), _collesctionsService.ElementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[1])
                {
                    double seconds = BenchmarkService.GetAddTime(new HashSet<int>(), _collesctionsService.ElementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[2])
                {
                    double seconds = BenchmarkService.GetAddTime(new LinkedList<int>(), _collesctionsService.ElementsCount);
                    results[type] = seconds;
                }
                else if (type == _acceptableTypes[3])
                {
                    double seconds = BenchmarkService.GetAddTime(new SortedSet<int>(), _collesctionsService.ElementsCount);
                    results[type] = seconds;
                }
            }

            return Ok(results);
        }
    }
}
