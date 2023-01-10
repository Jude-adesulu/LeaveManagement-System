using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    /// <summary>
    /// IRequestHandler (courtesy of MediatR) - ask what request its dealing with, in this case its GetLeaveAllocationDetailRequest and what should be returned
    /// </summary>
    public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, CreateLeaveAllocationDto>
    {
        /// <summary>
        /// ILeaveAllocationRepository - interface to communicate with the database
        /// IMapper - needs to do some mapping when getting the data from the database (convert from domain objects to DTOs)
        /// </summary>
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<CreateLeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
            //returns List of LeaveTypeDto, mapping into that will be the list of domain objects from the query
            return _mapper.Map<CreateLeaveAllocationDto>(leaveAllocation);
        }
    }
}
