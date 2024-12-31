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
    public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
    {
        return await _mediator.Send(new List.Query(), ct);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id, CancellationToken ct)
    {
        return await _mediator.Send(new Details.Query{ Id = id}, ct);
    }
    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] Activity activity, CancellationToken ct)
    {
        await _mediator.Send(new Create.Command{Activity = activity}, ct);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity([FromRoute] Guid id, [FromBody] Activity activity, CancellationToken ct)
    {
        activity.Id = id;
        await _mediator.Send( new Edit.Command { Activity = activity}, ct);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity([FromRoute] Guid id, CancellationToken ct)
    {
        await _mediator.Send( new Delete.Command { Id = id}, ct);
        return Ok();
    }

}