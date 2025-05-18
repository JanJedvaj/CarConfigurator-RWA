using CarConfigurator.DTOs;
using CarConfigurator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationController : ControllerBase
{
    private readonly CarConfiguratorContext _context;

    public ConfigurationController(CarConfiguratorContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create(ConfigurationDTO dto)
    {
        var config = new Configuration
        {
            CreationDate = dto.CreationDate,
            UserId = dto.UserId
        };
        _context.Configurations.Add(config);
        await _context.SaveChangesAsync();

        foreach (var compId in dto.CarComponentIds)
        {
            _context.ConfigurationCarComponents.Add(new ConfigurationCarComponent
            {
                ConfigurationId = config.Id,
                CarComponentId = compId
            });
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}