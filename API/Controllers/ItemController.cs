using API.DTOs.Item;
using API.Mappers;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/item")]
[ApiController]
public class ItemController : ControllerBase
{
    IItemRepository _repository;

    public ItemController(IItemRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<ItemDTO>))]
    public IActionResult GetAll()
    {
        var items = _repository.GetAll().Select(x => x.ToItemDTO());

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ItemDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] int id)
    {
        var item = _repository.GetById(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item.ToItemDTO());
    }

}