using AutoMapper;
using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    /// <summary>
    /// IRequestHandler (courtesy of MediatR) - ask what request its dealing with, in this case its GetLeaveRequestWithDetailRequest and what should be returned
    /// </summary>
    public class GetLeaveRequestWithDetailRequestHandler : IRequestHandler<GetLeaveRequestWithDetailRequest, LeaveRequestDto>
    {
        /// <summary>
        /// ILeaveRequestRepository - interface to communicate with the database
        /// IMapper - needs to do some mapping when getting the data from the database (convert from domain objects to DTOs)
        /// </summary>
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestWithDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestWithDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveRequestRepository.GetLeaveRequestsWithDetails(request.Id);
            //returns List of LeaveTypeDto, mapping into that will be the list of domain objects from the query
            return _mapper.Map<LeaveRequestDto>(leaveAllocation);
        }

    }
}
