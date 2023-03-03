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
    internal class CategoryGroupRepository : RepositoryBase<CategoryGroup>, ICategory_GroupRepository
    {
        public CategoryGroupRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(CategoryGroup category_Group)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.category_group SET cagro_name=@cagro_name,cagro_description=@cagro_description,cagro_type=@cagro_type,cagro_icon=@cagro_icon, cagro_icon_url= @cagro_icon_url  WHERE cagro_id= @cagro_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_id",
                        DataType = DbType.Int32,
                        Value = category_Group.CagroId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_name",
                        DataType = DbType.String,
                        Value = category_Group.CagroName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_description",
                        DataType = DbType.String,
                        Value = category_Group.CagroDescription
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_type",
                        DataType = DbType.String,
                        Value = category_Group.CagroType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon",
                        DataType = DbType.String,
                        Value = category_Group.CagroIcon
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon_url",
                        DataType = DbType.String,
                        Value = category_Group.CagroIconUrl
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    

        public IEnumerable<CategoryGroup> FindAllCategoryGroup()
        {
            IEnumerator<CategoryGroup> dataset = FindAll<CategoryGroup>("" +
                "              SELECT cagro_id as CagroId," +
                "                     cagro_name as CagroName," +
                "                     cagro_description as CagroDescription," +
                "                     cagro_type as CagroType," +
                "                     cagro_icon as CagroIcon, " +
                "                     cagro_icon_url as CagroIconUrl " +
                "              FROM master.category_group ORDER BY cagro_id;");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }
    

        public Task<IEnumerable<CategoryGroup>> FindAllCategoryGroupAsync()
        {
            throw new NotImplementedException();
        }

        public CategoryGroup FindCategoryGroupById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT cagro_id as CagroId," +
                "                     cagro_name as CagroName," +
                "                     cagro_description as CagroDescription," +
                "                     cagro_type as CagroType," +
                "                     cagro_icon as CagroIcon, " +
                "                     cagro_icon_url as CagroIconUrl" +
                "              FROM master.category_group where cagro_id=@cagro_id;",
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

            var dataSet = FindByCondition<CategoryGroup>(model);

            CategoryGroup? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(CategoryGroup category_Group)
        {
            SqlCommandModel model = new SqlCommandModel()
            {


                CommandText = "INSERT INTO master.category_group (cagro_name,cagro_description,cagro_type,cagro_icon, cagro_icon_url) VALUES (@cagro_name,@cagro_description,@cagro_type,@cagro_icon, @cagro_icon_url);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_name",
                        DataType = DbType.String,
                        Value = category_Group.CagroName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_description",
                        DataType = DbType.String,
                        Value = category_Group.CagroDescription
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_type",
                        DataType = DbType.String,
                        Value = category_Group.CagroType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon",
                        DataType = DbType.String,
                        Value = category_Group.CagroIcon
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_icon_url",
                        DataType = DbType.String,
                        Value = category_Group.CagroIconUrl
                    }
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            category_Group.CagroId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(CategoryGroup category_Group)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.category_group WHERE cagro_id=@cagro_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_id",
                        DataType = DbType.Int32,
                        Value = category_Group.CagroId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
