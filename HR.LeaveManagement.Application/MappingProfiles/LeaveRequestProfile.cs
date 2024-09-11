﻿using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
    public LeaveRequestProfile() 
        {
            CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
            CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>().ReverseMap();
        }
    }
}
