using FluentValidation;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class CreateLeaveAllocationValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            Include(new ILeaveAllocationDtoValidator(leaveTypeRepository));
        }
    }
}
