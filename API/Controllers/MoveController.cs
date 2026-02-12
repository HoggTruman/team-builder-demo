using API.DTOs.Move;
using API.Mappers;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/move")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly IMoveRepository _repository;

        public MoveController(IMoveRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<GetMoveDTO>))]
        public IActionResult GetAll()
        {
            var moves = _repository.GetAll().Select(x => x.ToMoveDTO());

            return Ok(moves);
        }

        [HttpGet("{pokemonId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(GetMoveDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMovesByPokemonId([FromRoute] int pokemonId)
        {
            var moves = _repository.GetMovesByPokemonId(pokemonId);

            if (moves == null)
            {
                return NotFound();
            }

            return Ok(moves.Select(x => x.ToMoveDTO()));
        }
    }
}