using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess.IRepositories
{
    public interface IUtilitiesRepository<T>
    {
        JsonResult InitiateDataTable(string draw,string length, string start, List<T> result);
    }
}