using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validateResult = await validator.ValidateAsync(request.LeaveRequestDto);
            if (validateResult.IsValid == false) throw new ValidatorException(validateResult);
            //get leaveRequest data from the database with the Id
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);

            if (request.LeaveRequestDto != null)
            {
                //map the request payload(source) to the data from the database(destination)
                _mapper.Map(request.LeaveRequestDto, leaveRequest);

                //save the updated leaveRequest (done by the mapper) to the database
                await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }
            else if(request.ChangeLeaveRequestApproval != null)
            {
                //save the updated leaveRequest to the database
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApproval.Approved);
            }

            //return unit
            return Unit.Value;
        }
    }
}
