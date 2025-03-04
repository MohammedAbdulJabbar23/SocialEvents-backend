using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;


public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
    public class CommandValidtor : AbstractValidator<Command>
    {
        public CommandValidtor()
        {
            RuleFor(x => x.Activity).SetValidator( new ActivityValidator());   
        }
    }
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                     x.UserName == _userAccessor.GetUsername());
            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };
            request.Activity.Attendees.Add(attendee);
            _context.Activities.Add(request.Activity);
            var result = await _context.SaveChangesAsync(ct) > 0;
            if(!result) return Result<Unit>.Failure("Failed to create activity");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}