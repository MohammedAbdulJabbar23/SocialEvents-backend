using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controller;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : BaseApiController
{
    private IMediator _mediator;
    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }ControllerBase

    [HttpGet]
    public async Task<IActionResult> GetActivities(CancellationToken ct)
    {
        return HandleResult(await _mediator.Send(new List.Query(), ct));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new Details.Query{ Id = id}, ct);
        if (result.IsSuccess && result.Value != null)
        {
            return Ok(result.Value);
        }
        if(result.IsSuccess && result.Value == null)
        {
            return NotFound();
        }
        return BadRequest(result.Error);
    }
    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] Activity activity, CancellationToken ct)
    {
        return HandleResult(await _mediator.Send(new Create.Command{Activity = activity}, ct));
        
    }
    [Authorize(Policy = "IsActivityHost")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity([FromRoute] Guid id, [FromBody] Activity activity, CancellationToken ct)
    {
        activity.Id = id;
        return HandleResult(await _mediator.Send( new Edit.Command { Activity = activity}, ct));
        
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity([FromRoute] Guid id, CancellationToken ct)
    {
        return HandleResult(await _mediator.Send( new Delete.Command { Id = id}, ct));
    }
    [HttpPost("{id}/attend")]
    public async Task<IActionResult> Attend(Guid id)
    {
        return HandleResult(await _mediator.Send(new UpdateAttendance.Command{Id = id}));
    }

}