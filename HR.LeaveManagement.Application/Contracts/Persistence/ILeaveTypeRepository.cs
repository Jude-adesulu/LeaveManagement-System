using HR.LeaveManagement.Clean.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
    }
}
