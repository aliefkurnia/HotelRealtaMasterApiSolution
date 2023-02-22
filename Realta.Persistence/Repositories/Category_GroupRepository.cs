using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class Category_GroupRepository : RepositoryBase<Category_Group>, ICategory_GroupRepository
    {
        public Category_GroupRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Category_Group category_Group)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.category_group SET cagro_name=@cagro_name,cagro_description=@cagro_description,cagro_type=@cagro_type,cagro_icon=@cagro_icon, cagro_icon_url= @cagro_icon_url  WHERE cagro_id= @cagro_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_id",
                        DataType = DbType.Int32,
                        Value = category_Group.cagro_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_name",
                        DataType = DbType.String,
                        Value = category_Group.cagro_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_description",
                        DataType = DbType.String,
                        Value = category_Group.cagro_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_type",
                        DataType = DbType.String,
                        Value = category_Group.cagro_type
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon",
                        DataType = DbType.String,
                        Value = category_Group.cagro_icon
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon_url",
                        DataType = DbType.String,
                        Value = category_Group.cagro_icon_url
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    

        public IEnumerable<Category_Group> FindAllCategory_Group()
        {
            IEnumerator<Category_Group> dataset = FindAll<Category_Group>("SELECT * from master.category_group ORDER BY cagro_id;");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }
    

        public Task<IEnumerable<Category_Group>> FindAllCategory_GroupAsync()
        {
            throw new NotImplementedException();
        }

        public Category_Group FindCategory_GroupById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.category_group where cagro_id=@cagro_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Category_Group>(model);

            Category_Group? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Category_Group category_Group)
        {
            SqlCommandModel model = new SqlCommandModel()
            {


                CommandText = "INSERT INTO master.category_group (cagro_name,cagro_description,cagro_type,cagro_icon, cagro_icon_url) VALUES (@cagro_name,@cagro_description,@cagro_type,@cagro_icon, @cagro_icon_url);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_name",
                        DataType = DbType.String,
                        Value = category_Group.cagro_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_description",
                        DataType = DbType.String,
                        Value = category_Group.cagro_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_type",
                        DataType = DbType.String,
                        Value = category_Group.cagro_type
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon",
                        DataType = DbType.String,
                        Value = category_Group.cagro_icon
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon_url",
                        DataType = DbType.String,
                        Value = category_Group.cagro_icon_url
                    }
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            category_Group.cagro_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(Category_Group category_Group)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.category_group WHERE cagro_id=@cagro_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_id",
                        DataType = DbType.Int32,
                        Value = category_Group.cagro_id
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
