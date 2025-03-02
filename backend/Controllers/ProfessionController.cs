using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessionController : EntityController<Profession, ProfessionDto>
{
    public ProfessionController(IEntityService<Profession, ProfessionDto> service) : base(service) {}
}