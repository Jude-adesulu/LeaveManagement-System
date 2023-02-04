using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Clean.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 15,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 5,
                    Name = "Test Sick"
                }
            };

            // initailize our mock repo
            var mockRepo = new Mock<ILeaveTypeRepository>();

            // mockRepo.Setup gives us access to the methods that can in ILeaveTypeRepository
            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(x => x.AddAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                //add the leaveType to the set of list of leaveTypes
                leaveTypes.Add(leaveType);
                return leaveType;
            });

            return mockRepo;
        }
    }
}
