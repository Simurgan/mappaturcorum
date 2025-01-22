using Microsoft.AspNetCore.Mvc;
using Mappa.Services;
using Mappa.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class EntityController<TEntity, TDto> : ControllerBase where TDto : BaseDto
{
    private readonly IEntityService<TEntity, TDto> _service;

    public EntityController(IEntityService<TEntity, TDto> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return Ok(item);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequest request)
    {
        var item = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRequest request)
    {
        var item = await _service.UpdateAsync(id, request);
        return Ok(item);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
