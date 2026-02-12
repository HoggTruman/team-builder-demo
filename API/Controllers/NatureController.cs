using API.DTOs.Nature;
using API.Mappers;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/nature")]
    [ApiController]
    public class NatureController : ControllerBase
    {
        private readonly INatureRepository _repository;

        public NatureController(INatureRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<NatureDTO>))]
        public IActionResult GetAll()
        {
            var natures = _repository.GetAll().Select(x => x.ToNatureDTO());

            return Ok(natures);
        }


        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(NatureDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var nature = _repository.GetById(id);

            if (nature == null)
                return NotFound();

            return Ok(nature.ToNatureDTO());
        }
    }
}