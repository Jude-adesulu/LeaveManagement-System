using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0).WithMessage("{PropertyName} must be valid")
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExist = await _leaveTypeRepository.Exist(id);
                    return !leaveTypeExist;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
