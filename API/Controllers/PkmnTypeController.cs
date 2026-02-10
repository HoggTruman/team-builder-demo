using API.DTOs.PkmnType;
using API.Interfaces.Repository;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/type")]
[ApiController]
public class PkmnTypeController : ControllerBase
{
    private readonly IPkmnTypeRepository _repository;

    public PkmnTypeController(IPkmnTypeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<PkmnTypeDTO>))]
    public IActionResult GetAll()
    {
        var pkmnTypes = _repository.GetAll().Select(x => x.ToPkmnTypeDTO());

        return Ok(pkmnTypes);
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(PkmnTypeDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] int id)
    {
        var pkmnType = _repository.GetById(id);

        if (pkmnType == null)
        {
            return NotFound();
        }

        return Ok(pkmnType.ToPkmnTypeDTO());
    }
    
}