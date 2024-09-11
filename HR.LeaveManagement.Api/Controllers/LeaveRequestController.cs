using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            var leaveRequestList = await mediator.Send(new GetLeaveRequestListQuery());
            return Ok(leaveRequestList);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestDetailsQuery { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand leaveRequest)
        {
            var id = await mediator.Send(leaveRequest);
            return CreatedAtAction(nameof(Get),new {Id = id});
        }

        // PUT api/<LeaveRequestController>/5
        [Authorize]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestCommand leaveRequestCommand)
        {
            await mediator.Send(leaveRequestCommand);
            return NoContent();
        }
        [Authorize]
        [HttpPut]
        [Route("CancelLeaveRequest")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> Put([FromBody] CancelLeaveRequestCommand cancelLeaveRequestCommand)
        {
            await mediator.Send(cancelLeaveRequestCommand);
            return NoContent();
        }
        [Authorize]
        [HttpPut]
        [Route("ChangeLeaveRequestApproval")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> Put([FromBody] ChangeLeaveRequestApprovalCommand changeLeaveRequestApprovalCommand)
        {
            await mediator.Send(changeLeaveRequestApprovalCommand);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> Delete(int id)
        {
            var deleteLeaveRequestCommand = new DeleteLeaveRequestCommand { Id = id};
            await mediator.Send(deleteLeaveRequestCommand);
            return NoContent();
        }
    }
}
