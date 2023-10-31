using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IIdentityRepository: IGenericRepository<User>
    {

  
    }
}