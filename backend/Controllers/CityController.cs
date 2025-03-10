using Microsoft.AspNetCore.Mvc;
using Mappa.Services;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService
        _service;

    public CityController(ICityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpPost]
    [Route("filter")]
    public async Task<IActionResult> GetAllFiltered([FromBody] CityFilterDto filter)
    {
        var items = await _service.GetAllFilteredAsync(filter);
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

    // [Authorize]
    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] CityCreateRequest request)
    // {
    //     try
    //     {
    //         var item = await _service.CreateAsync(request);
    //         return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return Conflict(new { Message = ex.Message });
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the exception (optional)
    //         return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
    //     }
    // }

    // [Authorize]
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(int id, [FromBody] CityUpdateRequest request)
    // {
    //     try
    //     {
    //         var item = await _service.UpdateAsync(id, request);
    //         return Ok(item);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return NotFound(new { Message = ex.Message });
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the exception (optional)
    //         return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
    //     }
    // }

    // [Authorize]
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     try
    //     {
    //         var result = await _service.DeleteAsync(id);
    //         if (!result)
    //             return NotFound(new {Message = $"Item with {id} not found"});

    //         return NoContent();
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the exception (optional)
    //         return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
    //     }
    // }

    [HttpGet]
    [Route("map")]
    public async Task<IActionResult> GetAllForMapAsync()
    {
        var items = await _service.GetAllForMapAsync();
        return Ok(items);
    }

}