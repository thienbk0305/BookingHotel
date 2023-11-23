using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Repositories;
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
        IImagesRepository ImagesRepository { get; }
        IEvalutesRepository EvalutesRepository { get; } 
        IBookingsRepository BookingsRepository { get; }
        IBookingDetailsRepository BookingDetailsRepository { get; }
        ISystemsRepository SystemsRepository { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
