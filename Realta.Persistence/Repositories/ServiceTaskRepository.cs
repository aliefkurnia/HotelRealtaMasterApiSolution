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
    internal class ServiceTaskRepository : RepositoryBase<ServiceTask>, IServiceTaskRepository
    {
        public ServiceTaskRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(ServiceTask serviceTask)
        {
            SqlCommandModel model = new SqlCommandModel()
            { 
                CommandText = "UPDATE master.service_task SET seta_name=@seta_name, seta_seq=@seta_seq WHERE seta_id= @seta_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_id",
                        DataType = DbType.Int32,
                        Value = serviceTask.SetaId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_name",
                        DataType = DbType.String,
                        Value = serviceTask.SetaName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_seq",
                        DataType = DbType.Int16,
                        Value = serviceTask.setaSeq
                    }
                }
            };
        _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<ServiceTask> FindAllServiceTask()
        {
            IEnumerator<ServiceTask> dataset = FindAll<ServiceTask>("SELECT seta_id as SetaId," +
                "                                                           seta_name as SetaName," +
                "                                                           seta_seq as SetaSeq " +
                "                                                    FROM master.service_task ORDER BY seta_id;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<ServiceTask>> FindAllServiceTaskAsync()
        {
            throw new NotImplementedException();
        }

        public ServiceTask FindServiceTaskById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT seta_id as SetaId," +
                "                     seta_name as SetaName," +
                "                     seta_seq as SetaSeq " +
                "              FROM master.service_task where seta_id=@seta_id;",
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

            var dataSet = FindByCondition<ServiceTask>(model);

            ServiceTask? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(ServiceTask serviceTask)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.service_task (seta_name,seta_seq) values (@seta_name,@seta_seq);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_name",
                        DataType = DbType.String,
                        Value = serviceTask.SetaName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_seq",
                        DataType = DbType.Int32,
                        Value = serviceTask.setaSeq
                    }
                }
            };
            serviceTask.SetaId= _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(ServiceTask serviceTask)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.service_task WHERE seta_id=@seta_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@seta_id",
                        DataType = DbType.Int32,
                        Value = serviceTask.SetaId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
