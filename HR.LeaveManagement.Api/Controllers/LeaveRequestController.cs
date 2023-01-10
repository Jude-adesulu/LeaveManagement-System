using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Clean.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestWithDetailRequest { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveRequestDto leaveRequest)
        {
            var command = new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequest };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, LeaveRequestDto = leaveRequest };
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT api/<LeaveRequestController>/ChangeApproval/5
        [HttpPut("ChangeApproval/{id}")]
        public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApproval)
        {
            var command = new UpdateLeaveRequestCommand {Id = id, ChangeLeaveRequestApproval = changeLeaveRequestApproval };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
            return NoContent();
        }
    }
}
