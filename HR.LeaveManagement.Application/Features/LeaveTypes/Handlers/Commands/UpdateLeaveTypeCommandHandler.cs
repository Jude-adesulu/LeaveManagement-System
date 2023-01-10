using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validateResult = await validator.ValidateAsync(request.LeaveTypeDto);
            if (validateResult.IsValid == false) throw new ValidatorException(validateResult);
            //get LeaveType data from the database with the Id
            var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeDto.Id);

            //map the request payload(source) to the data from the database(destination)
            _mapper.Map(request.LeaveTypeDto, leaveType);

            //save the updated leaveType (done by the mapper) to the database
            await _leaveTypeRepository.UpdateAsync(leaveType);

            //return unit
            return Unit.Value;
        }
    }
}
