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
            if (count > 100000)
            {
                return BadRequest(new {Error = "Значение не должно превышать 100000"});
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
    }
}
