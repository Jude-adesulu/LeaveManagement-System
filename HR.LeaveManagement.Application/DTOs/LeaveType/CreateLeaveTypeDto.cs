using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public class CreateLeaveTypeDto : ILeaveTypeDto
    {
        /// <summary>
        /// these fields are just an implementation of whats in ILeaveTypeDto
        /// </summary>
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
