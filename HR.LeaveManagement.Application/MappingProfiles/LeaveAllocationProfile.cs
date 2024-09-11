using AutoMapper;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile:Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
            CreateMap<CreateLeaveAllocationCommand,LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand,LeaveAllocation>();
        }
    }
}
