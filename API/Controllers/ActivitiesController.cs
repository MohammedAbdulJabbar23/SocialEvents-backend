using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controller;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private IMediator _mediator;
    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _mediator.Send(new List.Query());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id)
    {
        return await _mediator.Send(new Details.Query{ Id = id});
    }
    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] Activity activity)
    {
        await _mediator.Send(new Create.Command{Activity = activity});
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity([FromRoute] Guid id, [FromBody] Activity activity)
    {
        Console.WriteLine("CALLED");
        activity.Id = id;
        await _mediator.Send( new Edit.Command { Activity = activity});
        return Ok();
    }
}