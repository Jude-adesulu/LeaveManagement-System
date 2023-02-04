using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _createLeaveTypeCommandHandler;

        public CreateLeaveTypeCommandHandlerTest()
        {
            _leaveTypeRepositoryMock = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createLeaveTypeCommandHandler = new CreateLeaveTypeCommandHandler(_leaveTypeRepositoryMock.Object, _mapper);

            _leaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "TEST DTO"
            };
        }

        [Fact]
        public async Task CreateLeaveType()
        {
            var handler = new CreateLeaveTypeCommandHandler(_leaveTypeRepositoryMock.Object, _mapper);
            var result = await handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _leaveTypeRepositoryMock.Object.GetAllAsync();

            result.ShouldBeOfType<int>();
            leaveTypes.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InvalidLeaveType()
        {
            _leaveTypeDto.DefaultDays = -1;

            ValidatorException ex = await Should.ThrowAsync<ValidatorException>(
                async () =>
                    await _createLeaveTypeCommandHandler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None)
               );

            var leaveTypes = await _leaveTypeRepositoryMock.Object.GetAllAsync();

            leaveTypes.Count.ShouldBe(2);

            ex.ShouldNotBeNull();

        }
    }
}
