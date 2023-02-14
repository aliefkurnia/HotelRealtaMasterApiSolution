using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class Service_TaskRepository : RepositoryBase<Service_Task>, IService_TaskRepository
    {
        public Service_TaskRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Service_Task service_Task)
        {
            SqlCommandModel model = new SqlCommandModel()
            { 
                CommandText = "UPDATE master.service_task SET seta_name=@seta_name, seta_seq=@seta_seq WHERE seta_id= @seta_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_id",
                        DataType = DbType.Int32,
                        Value = service_Task.seta_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_name",
                        DataType = DbType.String,
                        Value = service_Task.seta_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_seq",
                        DataType = DbType.Int16,
                        Value = service_Task.seta_id
                    }
                }
            };
        _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Service_Task> FindAllService_Task()
        {
            IEnumerator<Service_Task> dataset = FindAll<Service_Task>("SELECT * FROM master.service_task ORDER BY seta_id;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Service_Task>> FindAllService_TaskAsync()
        {
            throw new NotImplementedException();
        }

        public Service_Task FindService_TaskById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.service_task where seta_id=@seta_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
    {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
    }
            };

            var dataSet = FindByCondition<Service_Task>(model);

            Service_Task? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Service_Task service_Task)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.service_task (seta_name,seta_seq) values (@seta_name,@seta_seq);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_name",
                        DataType = DbType.String,
                        Value = service_Task.seta_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_seq",
                        DataType = DbType.Int32,
                        Value = service_Task.seta_seq
                    }
                }
            };
            service_Task.seta_id= _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(Service_Task service_Task)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.service_task WHERE seta_id=@seta_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_id",
                        DataType = DbType.Int32,
                        Value = service_Task.seta_id
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
