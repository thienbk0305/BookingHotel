using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBookingHotel.Models;
using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Models.NewsModels;
using Microsoft.AspNetCore.Identity;

namespace APIBookingHotel.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentityRole,RoleResponse>().ReverseMap();
            CreateMap<ProfileView, User>().ReverseMap();
            CreateMap<NewsViewModel, New>().ReverseMap();
        }
    }
}