using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class GenderController : EntityController<Gender, GenderDto>
{
    public GenderController(IEntityService<Gender, GenderDto> service) : base(service) {}
}