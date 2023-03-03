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
    public class PriceItemsRepository : RepositoryBase<PriceItems>, IPriceItemsRepository
    {
        public PriceItemsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(PriceItems priceItems)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Master].[AddOrUpdatePriceItem]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@pritid",
                        DataType = DbType.Int32,
                        Value = priceItems.PritId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pritname",
                        DataType = DbType.String,
                        Value = priceItems.PritName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pritprice",
                        DataType = DbType.Decimal,
                        Value = priceItems.PritPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pritdescription",
                        DataType = DbType.String,
                        Value = priceItems.PritDescription
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prittype",
                        DataType = DbType.String,
                        Value = priceItems.PritType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@priticonurl",
                        DataType = DbType.String,
                        Value = priceItems.PritIconUrl
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        //public IEnumerable<Price_Items> FindAllPrice_Items()
        //{
        //    IEnumerator<Price_Items> dataset = FindAll<Price_Items>("SELECT * FROM master.Price_items ORDER BY prit_id;");

        //    while (dataset.MoveNext())
        //    {
        //        var data = dataset.Current;
        //        yield return data;
        //    }
        //}

        public IEnumerable<PriceItems> FindAllPriceItems()
        {
            IEnumerator<PriceItems> dataset = FindAll<PriceItems>("SELECT prit_id as PritId," +
                "                                                         prit_name as PritName," +
                "                                                         prit_price as PritPrice," +
                "                                                         prit_description as PritDescription," +
                "                                                         prit_type as PritType," +
                "                                                         prit_modified_date as PritModifiedDate," +
                "                                                         prit_icon_url as PritIconUrl" +
                "                                                  FROM master.Price_items ORDER BY prit_id;");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }

        }

        public Task<IEnumerable<PriceItems>> FindAllPriceItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PriceItems> FindPriceItemsByName(string name)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT prit_id as PritId," +
                "                     prit_name as PritName," +
                "                     prit_price as PritPrice," +
                "                     prit_description as PritDescription," +
                "                     prit_type as PritType," +
                "                     prit_modified_date as PritModifiedDate," +
                "                     prit_icon_url as PritIconUrl" +
                "              FROM master.price_items WHERE prit_name LIKE '%' + @prit_name + '%';",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
            {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_name",
                        DataType = DbType.String,
                        Value = name
                    }
            }
            };
            var dataSet = FindByCondition<PriceItems>(model);

            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;
            }
        }

        public PriceItems FindPriceItemsById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT prit_id as PritId," +
                "                     prit_name as PritName," +
                "                     prit_price as PritPrice," +
                "                     prit_description as PritDescription," +
                "                     prit_type as PritType," +
                "                     prit_modified_date as PritModifiedDate," +
                "                     prit_icon_url as PritIconUrl  FROM master.price_items where prit_id=@prit_id;",
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

            var dataSet = FindByCondition<PriceItems>(model);

            PriceItems? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(PriceItems priceItems)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.price_items (prit_name,prit_price,prit_description,prit_type,prit_icon_url) values (@prit_name,@prit_price,@prit_description,@prit_type,@prit_icon_url);" 
                + " SELECT cast(scope_identity() as int)"
                ,
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_name",
                        DataType = DbType.String,
                        Value = priceItems.PritName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_price",
                        DataType = DbType.Decimal,
                        Value = priceItems.PritPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_description",
                        DataType = DbType.String,
                        Value = priceItems.PritDescription
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_type",
                        DataType = DbType.String,
                        Value = priceItems.PritType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_icon_url",
                        DataType = DbType.String,
                        Value = priceItems.PritIconUrl
                    }
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            priceItems.PritId= _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(PriceItems price_Items)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.price_items WHERE prit_id=@prit_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_id",
                        DataType = DbType.Int32,
                        Value = price_Items.PritId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public int GetIdSequence()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT IDENT_CURRENT('Master.Price_items');",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };
            decimal id = _adoContext.ExecuteScalar<decimal>(model);
            _adoContext.Dispose();
            return (int)id;
        }
    }
}
