using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDoList.Identity.Application.Common.Services.JWTTokenGeneration;
using ToDoList.Identity.Application.Users.RequestDto;
using ToDoList.Identity.Domain.Entities;

namespace ToDoList.Identity.Application.Users.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {

        private readonly JWTTokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginCommandHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JWTTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return null; //NeedException

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                return null; //NeedException

            if (!Guid.TryParse(user.Id, out var userId))
                return null; //NeedException

            var token = await _tokenGenerator.Generate(user);

            return new LoginResult 
            { 
                Token = token, 
                UserId = userId 
            };
        }
    }
}
