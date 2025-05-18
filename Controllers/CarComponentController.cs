
using CarConfigurator.DTOs;
using CarConfigurator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CarComponentController : ControllerBase
{
    private readonly CarConfiguratorContext _context;

    public CarComponentController(CarConfiguratorContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarComponentDTO>>> GetAll() =>
        await _context.CarComponents.Select(cc => new CarComponentDTO
        {
            Id = cc.Id,
            Name = cc.Name,
            Description = cc.Description,
            ImagePath = cc.ImagePath,
            ComponentTypeId = cc.ComponentTypeId
        }).ToListAsync();

    [HttpPost]
    public async Task<ActionResult> Create(CarComponentDTO dto)
    {
        var cc = new CarComponent
        {
            Name = dto.Name,
            Description = dto.Description,
            ImagePath = dto.ImagePath,
            ComponentTypeId = dto.ComponentTypeId
        };
        _context.CarComponents.Add(cc);
        await _context.SaveChangesAsync();
        return Ok();
    }
}