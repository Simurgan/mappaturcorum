using Mappa.Dtos;
using Mappa.Entities;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class LanguageController : EntityController<Language, LanguageDto>
{
    public LanguageController(IEntityService<Language, LanguageDto> service) : base(service) {}
}