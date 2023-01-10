using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(x => x.NumberOfDays)
                 .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(x => x.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue");

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
