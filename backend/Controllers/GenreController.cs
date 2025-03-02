using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : EntityController<Genre, GenreDto>
{
    public GenreController(IEntityService<Genre, GenreDto> service) : base(service) {}
}