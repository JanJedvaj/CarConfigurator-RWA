using CarConfigurator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly CarConfiguratorContext _context;

    public UserController(CarConfiguratorContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll() =>
        await _context.Users.Select(u => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Phone = u.Phone
        }).ToListAsync();

    [HttpPost]
    public async Task<ActionResult> Create(UserDTO dto)
    {
        var user = new User
        {
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
