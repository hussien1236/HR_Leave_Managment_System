using HR.LeaveManagement.Api.Services;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: api/<LeaveTypeController>
        [HttpGet]
        public async Task<ActionResult> Get(string username)
        {
                var leaveTypes = await mediator.Send(new GetLeaveTypeListQuery());
            return Ok(leaveTypes);
        }

        // GET api/<LeaveTypeController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<LeaveTypeDetailsDto> Get(int id)
        {
            var leaveType = await mediator.Send(new GetLeaveTypeDetailsQuery(id));
            return leaveType;
        }

        // POST api/<LeaveTypeController>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand leaveType)
        {
            var response= await mediator.Send(leaveType);
            return CreatedAtAction(nameof(Get),new {id = response});
        }

        // PUT api/<LeaveTypeController>/5
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveTypeCommand leaveTypeCommand)
        {
            await mediator.Send(leaveTypeCommand);
            return NoContent();
        }

        // DELETE api/<LeaveTypeController>/5
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> Delete(int id)
        {
            var DeleteLeaveTypeCommand = new DeleteLeaveTypeCommand() { Id=id};
            await mediator.Send(DeleteLeaveTypeCommand);
            return NoContent();
        }
    }
}
