
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities;


public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }

    public class CommandValidtor : AbstractValidator<Command>
    {
        public CommandValidtor()
        {
            RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
        }
    }
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken ct)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
            if(activity == null)
            {
                return null;
            }

            _mapper.Map(request.Activity, activity);
            _context.Activities.Update(activity);
            var result = await _context.SaveChangesAsync(ct) > 0;
            if(!result)
            {
                return Result<Unit>.Failure("Failed to update the activity");
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}