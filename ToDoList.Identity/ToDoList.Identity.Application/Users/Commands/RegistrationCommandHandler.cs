using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Identity.Application.Common.Exceptions;
using ToDoList.Identity.Application.Common.Services.JWTTokenGeneration;
using ToDoList.Identity.Application.Users.RequestDto;
using ToDoList.Identity.Domain.Entities;

namespace ToDoList.Identity.Application.Users.Commands
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResult>
    {
        private readonly UserManager<User> _userManager;

        private readonly JWTTokenGenerator _tokenGenerator;

        public RegistrationCommandHandler(UserManager<User> userManager, JWTTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<RegistrationResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                BirthOfDate = request.DateOfBirth,
                Email = request.Email,
                UserName = request.UserName,
            };

            await _userManager.CreateAsync(user, request.Password);

            var token = await _tokenGenerator.Generate(user);

            return new RegistrationResult()
            {
                Token = token,
                UserId = user.Id
            };

        }
    }
}
