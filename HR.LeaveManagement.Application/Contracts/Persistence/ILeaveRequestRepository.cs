using HR.LeaveManagement.Clean.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestsWithDetails(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsAsync();
        Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
    }
}
