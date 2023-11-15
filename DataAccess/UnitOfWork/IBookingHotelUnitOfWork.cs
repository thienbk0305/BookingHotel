﻿using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IBookingHotelUnitOfWork
    {
        IIdentityRepository Identity { get; }
        IContactRepository ContactRepository { get; }
        ICustomerRepository CustomerRepository { get; }

        INewsRepository NewsRepository { get; }
        IRoomsRepository RoomsRepository { get; }
        IServicesRepository ServicesRepository { get; }
        IHotelsRepository HotelsRepository { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
