using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands
{
    //Note -its not neccessary to have IRequest to have a datatype if its not returning anything
    public class DeleteLeaveTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
