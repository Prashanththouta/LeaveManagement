using LeaveManagement.Data;
using LeaveManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Contracts
{
    interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
    }
}
