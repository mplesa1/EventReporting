using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.DataAccess
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
