using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class TypeController : EntityController<Entities.Type, TypeDto>
{
    public TypeController(IEntityService<Entities.Type, TypeDto> service) : base(service) {}
}