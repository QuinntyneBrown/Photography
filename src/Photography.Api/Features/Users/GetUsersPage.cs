using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Photography.Api.Extensions;
using Photography.Api.Core;
using Photography.Api.Interfaces;
using Photography.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Photography.Api.Features
{
    public class GetUsersPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<UserDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPhotographyDbContext _context;
        
            public Handler(IPhotographyDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from user in _context.Users
                    select user;
                
                var length = await _context.Users.CountAsync();
                
                var users = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = users
                };
            }
            
        }
    }
}
