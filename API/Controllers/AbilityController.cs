using API.DTOs.Ability;
using API.Mappers;
using API.Repository;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/ability")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        private readonly IAbilityRepository _repository;

        public AbilityController(IAbilityRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<AbilityDTO>))]
        public IActionResult GetAll()
        {
            var abilities = _repository.GetAll().Select(x => x.ToAbilityDTO());

            return Ok(abilities);
        }


        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(AbilityDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var ability = _repository.GetById(id);

            if (ability == null)
                return NotFound();

            return Ok(ability.ToAbilityDTO());
        }
    }
}