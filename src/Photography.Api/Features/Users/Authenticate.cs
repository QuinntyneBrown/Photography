using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Photography.Api.Core;
using Photography.Api.Interfaces;
using Photography.Api.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Photography.Api.Features
{
    public class Authenticate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotNull();
                RuleFor(x => x.Password).NotNull();
            }
        }

        public record Request(string Username, string Password) : IRequest<Response>;

        public record Response(string AccessToken, System.Guid UserId);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IPhotographyDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly ITokenProvider _tokenProvider;
            private readonly ITokenBuilder _tokenBuilder;

            public Handler(IPhotographyDbContext context, ITokenProvider tokenProvider, IPasswordHasher passwordHasher, ITokenBuilder tokenBuilder)
            {
                _context = context;
                _tokenProvider = tokenProvider;
                _passwordHasher = passwordHasher;
                _tokenBuilder = tokenBuilder;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(x => x.Username == request.Username);

                if (user == null)
                    throw new Exception();

                if (!ValidateUser(user, _passwordHasher.HashPassword(user.Salt, request.Password)))
                    throw new Exception();

                _tokenBuilder
                    .AddClaim(new System.Security.Claims.Claim(Constants.ClaimTypes.UserId, $"{user.UserId}"))
                    .AddUsername(user.Username);

                return new(_tokenBuilder.Build(), user.UserId);

            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }
        }
    }
}
