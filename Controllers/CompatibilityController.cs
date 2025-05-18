using CarConfigurator.DTOs;
using CarConfigurator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CompatibilityController : ControllerBase
{
    private readonly CarConfiguratorContext _context;

    public CompatibilityController(CarConfiguratorContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> AddCompatibility(CarComponentCompatibilityDTO dto)
    {
        var comp = new CarComponentCompatibility
        {
            CarComponentId1 = dto.CarComponentId1,
            CarComponentId2 = dto.CarComponentId2
        };
        _context.CarComponentCompatibilities.Add(comp);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{componentId}")]
    public async Task<ActionResult<IEnumerable<CarComponentDTO>>> GetCompatible(int componentId)
    {
        var compatibleIds = await _context.CarComponentCompatibilities
            .Where(c => c.CarComponentId1 == componentId || c.CarComponentId2 == componentId)
            .Select(c => c.CarComponentId1 == componentId ? c.CarComponentId2 : c.CarComponentId1)
            .Distinct()
            .ToListAsync();

        var components = await _context.CarComponents
            .Where(cc => compatibleIds.Contains(cc.Id))
            .Select(cc => new CarComponentDTO
            {
                Id = cc.Id,
                Name = cc.Name,
                Description = cc.Description,
                ImagePath = cc.ImagePath,
                ComponentTypeId = cc.ComponentTypeId
            }).ToListAsync();

        return components;
    }
}