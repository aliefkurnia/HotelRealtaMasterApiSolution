using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IServiceTaskRepository
    {
        IEnumerable<ServiceTask> FindAllServiceTask();
        Task<IEnumerable<ServiceTask>> FindAllServiceTaskAsync();
        ServiceTask FindServiceTaskById(int id);
        void Insert(ServiceTask serviceTask);
        void Edit(ServiceTask serviceTask);
        void Remove(ServiceTask serviceTask);
    }
}
