using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Photography.Api.Models;
using Photography.Api.Core;
using Photography.Api.Interfaces;

namespace Photography.Api.Features
{
    public class RemoveUser
    {
        public class Request: IRequest<Response>
        {
            public Guid UserId { get; set; }
        }

        public class Response: ResponseBase
        {
            public UserDto User { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPhotographyDbContext _context;
        
            public Handler(IPhotographyDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(x => x.UserId == request.UserId);
                
                _context.Users.Remove(user);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    User = user.ToDto()
                };
            }
            
        }
    }
}
