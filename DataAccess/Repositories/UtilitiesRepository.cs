using DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UtilitiesRepository<T> : Controller,IUtilitiesRepository<T>
    {
        public UtilitiesRepository()
        {
            
        }
        public JsonResult InitiateDataTable(string draw,string length, string start, List<T> result)
        {
            try
            {
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                //Get total records
                recordsTotal = result.Count();
                //Paging   
                var dataResult = result.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, dataResult });
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}