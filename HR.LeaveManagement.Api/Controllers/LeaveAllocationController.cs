using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var LeaveAllocations = await mediator.Send(new GetLeaveAllocationsQuery());
            return Ok(LeaveAllocations);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
        {
            var LeaveAllocation = await mediator.Send(new GetLeaveAllocationDetailsQuery { Id = id});
            return Ok(LeaveAllocation);
        }

        // POST api/<LeaveAllocationController>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand createLeaveAllocationCommand)
        {
            var response = await mediator.Send(createLeaveAllocationCommand);
            return CreatedAtAction(nameof(Get),new {id = response});
        }

        // PUT api/<LeaveAllocationController>/5
        [Authorize]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationCommand updateLeaveAllocationCommand)
        {
            await mediator.Send(updateLeaveAllocationCommand);
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var Command = new DeleteLeaveAllocationCommand { Id = id};
            await mediator.Send(Command);
            return NoContent();
        }
    }
}
