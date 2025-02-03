using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class EthnicityController : EntityController<Ethnicity, EthnicityDto>
{
    public EthnicityController(IEntityService<Ethnicity, EthnicityDto> service) : base(service) {}
}