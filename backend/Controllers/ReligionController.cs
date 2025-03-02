using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class ReligionController : EntityController<Religion, ReligionDto>
{
    public ReligionController(IEntityService<Religion, ReligionDto> service) : base(service) {}
}