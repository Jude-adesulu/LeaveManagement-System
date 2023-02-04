using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Clean.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Queries
{
    public class GetLeaveTypeListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        //mock LeaveTypeRepo (so it don't inject in the real leaveType repository
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;

        public GetLeaveTypeListRequestHandlerTest()
        {
            _leaveTypeRepositoryMock = MockLeaveTypeRepository.GetLeaveTypeRepository();

            //initialize the mapper configuration object
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveTypeList()
        {
            var handler = new GetLeaveTypeListRequestHandler(_leaveTypeRepositoryMock.Object, _mapper);
            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}
