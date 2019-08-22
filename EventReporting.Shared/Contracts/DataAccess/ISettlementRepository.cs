using EventReporting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReporting.Shared.Contracts.DataAccess
{
    public interface ISettlementRepository
    {
        Task<ICollection<Settlement>> FindAllAsync();

        Task CreateAsync(Settlement settlement);

        void UpdateAsync(Settlement settlement);

        Task<Settlement> FindByIdAsync(int id);

        void DeleteAsync(Settlement city);
    }
}
