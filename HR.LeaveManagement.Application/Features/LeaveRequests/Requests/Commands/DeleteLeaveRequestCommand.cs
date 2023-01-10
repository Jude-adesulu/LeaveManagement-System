using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    //its not neccessary to have IRequest to have a datatype if its not returning anything
    public class DeleteLeaveRequestCommand : IRequest
    {
        public int Id { get; set; }
    }
}
