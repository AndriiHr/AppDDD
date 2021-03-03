using System.Threading.Tasks;
using App.Api.Attributes;
using App.Application.Feedbacks.Commands;
using App.Application.Feedbacks.Queries;
using App.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FeedbackController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Role = UserRole.Manager)]
        public async Task<IActionResult> AssignFeedback([FromBody] AssignFeedbackRequest request)
        {
            await _mediator.Send(new AssignFeedbackToProjectCommand(request.Feedback, request.ProjectId));

            return Ok();
        }

        [Route("{feedbackId}")]
        [HttpGet]
        public async Task<IActionResult> GetFeedback([FromRoute] int feedbackId)
        {
            var projects = await _mediator.Send(new GetFeedbackQuery(feedbackId));

            return Ok(projects);
        }

        [HttpPut]
        [Authorize(Role = UserRole.Manager)]
        public async Task<IActionResult> UpdateFeedback([FromBody] EditFeedbackRequest request)
        {
            var project = await _mediator.Send(new EditFeedbackCommand(request.Feedback));

            return Ok(project);
        }

        [Route("{feedbackId}")]
        [HttpDelete]
        [Authorize(Role = UserRole.Manager)]
        public async Task<IActionResult> DeleteFeedback([FromRoute] int feedbackId)
        {
            await _mediator.Send(new DeleteFeedbackCommand(feedbackId));
            return Ok();
        }
    }
}