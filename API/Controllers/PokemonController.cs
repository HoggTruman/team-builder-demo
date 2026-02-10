using API.DTOs.Pokemon;
using API.Interfaces.Repository;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _repository;

        public PokemonController(IPokemonRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<PokemonDTO>))]
        public IActionResult GetAll()
        {
            var pokemon = _repository.GetAll().Select(x => x.ToPokemonDTO());

            return Ok(pokemon);
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(PokemonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] int id)
        {
            var pokemon = _repository.GetById(id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon.ToPokemonDTO());
        }
    }
}