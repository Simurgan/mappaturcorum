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
        try
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }
        catch (ArgumentException ex) when (ex.Message.Contains($"Entity with ID {id} not found."))
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequest request)
    {
        try
        {
            var item = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
        catch (ArgumentException ex) when (ex.Message.Contains($"An entity with the name '{request.Name}' already exists."))
        {
            return Conflict(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRequest request)
    {
        try
        {
            var item = await _service.UpdateAsync(id, request);
            return Ok(item);
        }
        catch (ArgumentException ex) when (ex.Message.Contains($"Entity with ID {id} not found."))
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex) when (ex.Message.Contains($"Entity with ID {id} not found."))
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}
