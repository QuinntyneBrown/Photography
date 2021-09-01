using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Photography.Api.Models;
using Photography.Api.Core;
using Photography.Api.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Photography.Api.Features
{
    public class CreatePhoto
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
                _configuration = configuration;
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var photo = new Photo(request.Photo.Name);
                
                _context.Photos.Add(photo);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    Photo = photo.ToDto(_configuration)
                };
            }
            
        }
    }
}
