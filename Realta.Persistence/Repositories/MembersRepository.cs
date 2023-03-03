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
    internal class MembersRepository : RepositoryBase<Members>, IMembersRepository
    {
        public MembersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Members members)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.members SET memb_name=@memb_name,memb_description=@memb_description WHERE memb_name= @memb_name;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@memb_name",
                        DataType = DbType.String,
                        Value = members.MembName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@memb_description",
                        DataType = DbType.String,
                        Value = members.MembDescription
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Members> FindAllMembers()
        {
            IEnumerator<Members> dataset = FindAll<Members>("SELECT memb_name as MembName," +
                "                                                   memb_description as MembDescription" +
                "                                            FROM master.Members ORDER BY memb_name;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Members>> FindAllMembersAsync()
        {
            throw new NotImplementedException();
        }

        public Members FindMembersById(string id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT memb_name as MembName," +
                "                     memb_description as MembDescription " +
                "              FROM master.members where memb_name=@memb_name;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@memb_name",
                        DataType = DbType.String,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Members>(model);

            Members? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Members members)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.members (memb_name,memb_description) values (@memb_name,@memb_description);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@memb_name",
                        DataType = DbType.String,
                        Value = members.MembName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@memb_description",
                        DataType = DbType.String,
                        Value = members.MembDescription
                    }
                }
            };
            _adoContext.Dispose();
        }

        public void Remove(Members members)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.members WHERE memb_name=@memb_name;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@memb_name",
                        DataType = DbType.String,
                        Value = members.MembName
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
