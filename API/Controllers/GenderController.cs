using API.DTOs.Gender;
using API.Mappers;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/gender")]
[ApiController]
public class GenderController : ControllerBase
{
    IGenderRepository _repository;

    public GenderController(IGenderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<GenderDTO>))]
    public IActionResult GetAll()
    {
        var genders = _repository.GetAll().Select(x => x.ToGenderDTO());

        return Ok(genders);
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(GenderDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] int id)
    {
        var gender = _repository.GetById(id);

        if (gender == null)
        {
            return NotFound();
        }

        return Ok(gender.ToGenderDTO());
    }

}