using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validateResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            if (validateResult.IsValid == false) throw new ValidatorException(validateResult);
            //get leaveAllocation data from the database with the Id
            var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id);

            //map the request payload(source) to the data from the database(destination)
            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            //save the updated LeaveAllocation (done by the mapper) to the database
            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            //return unit
            return Unit.Value;
        }
    }
}
