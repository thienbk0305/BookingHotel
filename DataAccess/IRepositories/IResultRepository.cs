using System.Collections.Generic;

namespace DataAccess.IRepositories
{
    public interface IResultRepository
    {
        List<string> Messages { get; set; }

        bool Succeeded { get; set; }
    }

    public interface IResult<out T> : IResultRepository
    {
        T Data { get; }
    }
}