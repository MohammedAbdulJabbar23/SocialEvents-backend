using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controller;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly DataContext _ctx;
    public ActivitiesController(DataContext context)
    {
        _ctx = context;    
    }
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _ctx.Activities.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id)
    {
        return await _ctx.Activities.FindAsync(id);
    }
}