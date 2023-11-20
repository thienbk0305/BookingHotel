using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBookingHotel.Models;
using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Models.CustomersModels;
using DataAccess.Models.HotelsModels;
using DataAccess.Models.NewsModels;
using DataAccess.Models.RoomsModels;
using DataAccess.Models.ServicesModels;
using DataAccess.Models.SystemsModels;
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
            CreateMap<RoomsViewModel, Room>().ReverseMap();
            CreateMap<ServicesViewModel, Service>().ReverseMap();
            CreateMap<HotelsViewModel, Hotel>().ReverseMap();
            CreateMap<CustomersViewModel, Customer>().ReverseMap();
            CreateMap<HRSViewModel, HotelRoomService>().ReverseMap();
            //CreateMap<BookingsViewModel, Booking>().ReverseMap();
        }
    }
}