using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IMembersRepository
    {
        IEnumerable<Members> FindAllMembers();
        Task<IEnumerable<Members>> FindAllMembersAsync();
        Members FindMembersById(string id);
        void Insert(Members members);
        void Edit(Members members);
        void Remove(Members members);
    }
}
