using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Clean.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeValidator();
            var validateResult = await validator.ValidateAsync(request.LeaveTypeDto);
            if (validateResult.IsValid == false) throw new ValidatorException(validateResult);
            //map leaveTypeDto into LeaveType domain object
            var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);

            leaveType = await _leaveTypeRepository.AddAsync(leaveType);

            return leaveType.Id;

        }
    }
}
