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
    public class Price_ItemsRepository : RepositoryBase<Price_Items>, IPrice_ItemsRepository
    {
        public Price_ItemsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Price_Items price_Items)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.price_items SET prit_name=@prit_name,prit_price=@prit_price, prit_description=@prit_description, prit_type=@prit_type WHERE prit_id= @prit_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_id",
                        DataType = DbType.Int32,
                        Value = price_Items.prit_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_name",
                        DataType = DbType.String,
                        Value = price_Items.prit_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_price",
                        DataType = DbType.Decimal,
                        Value = price_Items.prit_price
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_description",
                        DataType = DbType.String,
                        Value = price_Items.prit_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_type",
                        DataType = DbType.String,
                        Value = price_Items.prit_type
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Price_Items> FindAllPrice_Items()
        {
            IEnumerator<Price_Items> dataset = FindAll<Price_Items>("SELECT * FROM master.Price_items ORDER BY prit_id;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Price_Items>> FindAllPrice_ItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Price_Items FindPrice_ItemsById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.price_items where prit_id=@prit_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
    {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
    }
            };

            var dataSet = FindByCondition<Price_Items>(model);

            Price_Items? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Price_Items price_Items)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.price_items (prit_name,prit_price,prit_description,prit_type) values (@prit_name,@prit_price,@prit_description,@prit_type);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_name",
                        DataType = DbType.String,
                        Value = price_Items.prit_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_price",
                        DataType = DbType.Decimal,
                        Value = price_Items.prit_price
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_description",
                        DataType = DbType.String,
                        Value = price_Items.prit_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_type",
                        DataType = DbType.String,
                        Value = price_Items.prit_type
                    }
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            price_Items.prit_id= _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(Price_Items price_Items)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.price_items WHERE prit_id=@prit_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_id",
                        DataType = DbType.Int32,
                        Value = price_Items.prit_id
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
