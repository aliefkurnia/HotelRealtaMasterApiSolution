using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realta.Domain.RequestFeatures;
using Realta.Persistence.Repositories.RepositoryExtensions;

namespace Realta.Persistence.Repositories
{
    internal class PolicyRepository : RepositoryBase<Policy>, IPolicyRepository
    {
        public PolicyRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Policy policy)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.Policy SET poli_name=@poli_name,poli_description=@poli_description WHERE poli_id= @poli_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_id",
                        DataType = DbType.Int32,
                        Value = policy.PoliId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_name",
                        DataType = DbType.String,
                        Value = policy.PoliName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_description",
                        DataType = DbType.String,
                        Value = policy.PoliDescription
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Policy> FindAllPolicy()
        {
            IEnumerator<Policy> dataset = FindAll<Policy>("" +
                                                          "SELECT poli_id as PoliId," +
                                                          "poli_name as PoliName," +
                                                          "poli_description as PoliDescription  FROM " +
                                                          "master.policy ORDER BY poli_id;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Policy>> FindAllPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Policy FindPolicyById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT poli_id as PoliId," +
                "                     poli_name as PoliName," +
                "                     poli_description as PoliDescription " +
                "              FROM master.policy where poli_id=@poli_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
    {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
    }
            };

            var dataSet = FindByCondition<Policy>(model);

            Policy? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public IEnumerable<Policy> FindPolicyByName(string name)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.policy WHERE poli_name LIKE '%' + @poli_name + '%';",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
{
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_name",
                        DataType = DbType.String,
                        Value = name
                    }
}
            };
            var dataSet = FindByCondition<Policy>(model);

            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;
            }
        }

        public void Insert(Policy policy)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.policy (poli_name,poli_description) values (@poli_name,@poli_description);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_name",
                        DataType = DbType.String,
                        Value = policy.PoliName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_description",
                        DataType = DbType.String,
                        Value = policy.PoliDescription
                    }
                }
            };
            policy.PoliId= _adoContext.ExecuteScalar<int>(model);
            //_adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Policy policy)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.policy WHERE poli_id=@poli_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poli_id",
                        DataType = DbType.Int32,
                        Value = policy.PoliId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public async Task<PagedList<Policy>> GetPolicyPageList(PolicyParameter policyParameter)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT poli_id as PoliId, " +
                              "poli_name as PoliName, " +
                              "poli_description as PoliDescription FROM " +
                              "master.policy ORDER BY poli_id; ",

                // "OFFSET @pageNo ROWS FETCH NEXT  @pageSize ROWS ONLY;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };
            var policy = await GetAllAsync<Policy>(model);
            // var totalRow = FindAllPolicy().Count();

            //var policySearch = policy.Where(p => p.PoliName
            //    .ToLower()
            //    .Contains(policyParameter.SearchTerm == null ? "" : policyParameter.SearchTerm.Trim().ToLower()));

             policy = policy.AsQueryable()
                .SearchPolicy(policyParameter.SearchTerm)
                .Sort(policyParameter.OrderBy);

            return PagedList<Policy>.ToPagedList(policy.ToList(), policyParameter.PageNumber,
                policyParameter.PageSize);
        }
    }
}
