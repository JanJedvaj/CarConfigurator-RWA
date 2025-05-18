using CarConfigurator.DTOs;
using CarConfigurator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ComponentTypeController : ControllerBase
{
    private readonly CarConfiguratorContext _context;

    public ComponentTypeController(CarConfiguratorContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComponentTypeDTO>>> GetAll() =>
        await _context.ComponentTypes.Select(ct => new ComponentTypeDTO
        {
            Id = ct.Id,
            Name = ct.Name
        }).ToListAsync();
}