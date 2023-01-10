using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    /// <summary>
    /// IRequestHandler (courtesy of MediatR) - ask what request its dealing with, in this case its GetLeaveTypeListRequest and what should be returned
    /// </summary>
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        /// <summary>
        /// ILeaveTypeRepository - interface to communicate with the database
        /// IMapper - needs to do some mapping when getting the data from the database (convert from domain objects to DTOs)
        /// </summary>

        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetAllAsync();

            //returns List of LeaveTypeDto, mapping into that will be the list of domain objects from the query
            return _mapper.Map<List<LeaveTypeDto>>(leaveType);
        }
    }
}
