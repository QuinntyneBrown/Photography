using Photography.Api.Core;
using Photography.Api.Interfaces;
using Photography.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Photography.Api.Features
{
    public class CurrentUser
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public UserDto User { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IPhotographyDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IPhotographyDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return new();
                }

                var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value);

                User user = _context.Users
                    .Single(x => x.UserId == userId);

                return new()
                {
                    User = user.ToDto()
                };
            }
        }
    }
}