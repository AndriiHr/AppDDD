using System;
using System.IO;
using System.Threading.Tasks;
using App.Api.Attributes;
using App.Application.Users;
using App.Application.Users.Commands;
using App.Application.Users.Queries;
using App.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IMediator mediator, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("")]
        [HttpPost]
        [Authorize(Role = UserRole.Manager)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var userDto = _mapper.Map<UserDto>(request);
            await _mediator.Send(new RegisterUserCommand(userDto));

            return Ok();
        } 

        [Route("users")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromRoute] bool active = true)
        {
            var users = await _mediator.Send(new GetUsersQuery(active));

            return Ok(users);
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = await _mediator.Send(new GetUserQuery(userId));

            return Ok(user);
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            await _mediator.Send(new UpdateUserCommand(request.User));
            return Ok();
        }

        [Route("{userId}")]
        [HttpDelete]
        [Authorize(Role = UserRole.Manager)]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            await _mediator.Send(new DeleteUserCommand(userId));

            return Ok();
        }

        [Route("{userId}/{projectId}")]
        [HttpPut]
        [Authorize(Role = UserRole.Manager)]
        public async Task<IActionResult> AssignProjectToUser([FromRoute] int userId, [FromRoute] int projectId)
        {
            await _mediator.Send(new AssignUserToProjectCommand(userId, projectId));

            return Ok();
        }

        [Route("image")]
        [HttpPut]
        public async Task<IActionResult> UpdateImageProfile([FromBody] UpdateImageProfileRequest request)
        {
            string filePath = UploadedFile(request);
            await _mediator.Send(new UpdateUserProfileImageCommand(request.UserId, filePath));

            return Ok();
        }

        private string UploadedFile(UpdateImageProfileRequest model)
        {
            string uniqueFileName = null;

            if (model.ImageProfile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageProfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageProfile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

    }
}