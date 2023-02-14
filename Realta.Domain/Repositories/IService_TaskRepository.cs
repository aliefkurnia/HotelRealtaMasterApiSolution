using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IService_TaskRepository
    {
        IEnumerable<Service_Task> FindAllService_Task();
        Task<IEnumerable<Service_Task>> FindAllService_TaskAsync();
        Service_Task FindService_TaskById(int id);
        void Insert(Service_Task service_Task);
        void Edit(Service_Task service_Task);
        void Remove(Service_Task service_Task);
    }
}
