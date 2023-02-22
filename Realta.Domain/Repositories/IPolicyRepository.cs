using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IPolicyRepository
    {
        IEnumerable<Policy> FindAllPolicy();
        Task<IEnumerable<Policy>> FindAllPolicyAsync();
        Policy FindPolicyById(int id);
        void Insert(Policy policy);
        void Edit(Policy policy);
        void Remove(Policy policy);
    }
}
