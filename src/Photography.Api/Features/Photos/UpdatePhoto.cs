using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Photography.Api.Core;
using Photography.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Photography.Api.Features
{
    public class UpdatePhoto
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Photo).NotNull();
                RuleFor(request => request.Photo).SetValidator(new PhotoValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public PhotoDto Photo { get; set; }
        }

        public class Response: ResponseBase
        {
            public PhotoDto Photo { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IPhotographyDbContext _context;
            private readonly IConfiguration _configuration;

            public Handler(IPhotographyDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var photo = await _context.Photos.SingleAsync(x => x.PhotoId == request.Photo.PhotoId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    Photo = photo.ToDto(_configuration)
                };
            }
            
        }
    }
}
