using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDoList.Identity.Application.Common.Services.JWTTokenGeneration;
using ToDoList.Identity.Application.Users.RequestDto;
using ToDoList.Identity.Domain.Entities;

namespace ToDoList.Identity.Application.Users.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResult>
    {

        private readonly JWTTokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginQueryHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JWTTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return null; //NeedException

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                return null; //NeedException

            var token = await _tokenGenerator.Generate(user);

            return new LoginResult 
            { 
                Token = token, 
                UserId = user.Id 
            };
        }
    }
}
