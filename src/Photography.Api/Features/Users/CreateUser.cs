using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Photography.Api.Models;
using Photography.Api.Core;
using Photography.Api.Interfaces;

namespace Photography.Api.Features
{
    public class CreateUser
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.User).NotNull();
                RuleFor(request => request.User).SetValidator(new UserValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public UserDto User { get; set; }
        }

        public class Response: ResponseBase
        {
            public UserDto User { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPhotographyDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
        
            public Handler(IPhotographyDbContext context, IPasswordHasher passwordHasher)
            {
                _context = context;
                _passwordHasher = passwordHasher;
            }
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = new User(request.User.Username, request.User.Password, _passwordHasher);
                
                _context.Users.Add(user);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    User = user.ToDto()
                };
            }
            
        }
    }
}
