using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    /// <summary>
    /// IRequest(courtesy of MediatR) - it ask what this request (using this datatype) should expect in return
    /// Note - DTOs are what will come in and go out, never the Domain Objects
    /// </summary>
    public class GetLeaveAllocationDetailRequest : IRequest<CreateLeaveAllocationDto>
    {
        public int Id { get; set; }
    }
}
