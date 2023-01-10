using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
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
    /// IRequestHandler (courtesy of MediatR) - ask what request its dealing with, in this case its GetLeaveRequestListRequest and what should be returned
    /// </summary>
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        /// <summary>
        /// ILeaveRequestRepository - interface to communicate with the database
        /// IMapper - needs to do some mapping when getting the data from the database (convert from domain objects to DTOs)
        /// </summary>

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestsAsync();
            //returns List of LeaveRequestListDto, mapping into that will be the list of domain objects from the query
            return _mapper.Map<List<LeaveRequestListDto>>(leaveRequest);
        }
    }
}
