using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.Interface;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class StocksRepository : RepositoryBase<Stocks>, IStockRepository
    {
        public StocksRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Stocks stocks)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE purchasing.stocks SET " +
                "stock_name=@stockName, stock_description=@stockDesc, stock_quantity=@stockQty, " +
                "stock_reorder_point=@stockRP, stock_used=@stockUsed, stock_scrap=@stockScrap, stock_size=@stockSize, " +
                "stock_color=@stockColor, stock_modified_date=@stockModifiedDate" +
                " WHERE stock_id=@stockId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockId",
                        DataType = DbType.Int32,
                        Value = stocks.stock_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockName",
                        DataType = DbType.String,
                        Value = stocks.stock_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockDesc",
                        DataType = DbType.String,
                        Value = stocks.stock_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockQty",
                        DataType = DbType.Int16,
                        Value = stocks.stock_quantity
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockRP",
                        DataType = DbType.Int16,
                        Value = stocks.stock_reorder_point
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockUsed",
                        DataType = DbType.Int16,
                        Value = stocks.stock_used
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockScrap",
                        DataType = DbType.Int16,
                        Value = stocks.stock_scrap
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockSize",
                        DataType = DbType.String,
                        Value = stocks.stock_size
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockColor",
                        DataType = DbType.String,
                        Value = stocks.stock_color
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockModifiedDate",
                        DataType = DbType.DateTime,
                        Value = stocks.stock_modified_date
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Stocks> FindAllStocks()
        {
            IEnumerator<Stocks> dataSet = FindAll<Stocks>("SELECT * FROM Purchasing.stocks");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;

            }
        }

        public async Task<IEnumerable<Stocks>> FindAllStocksAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Purchasing.stocks;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<Stocks> dataSet = FindAllAsync<Stocks>(model);

            var item = new List<Stocks>();


            while (await dataSet.MoveNextAsync())
              {
                item.Add(dataSet.Current);
              }


            return item;
        }

        public Stocks FindStocksById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM purchasing.stocks where stock_id=@stockId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Stocks>(model);

            Stocks? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(Stocks stocks)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO region (stock_name, stock_description, stock_quantity, stock_reorder_point, stock_used, " +
                "stock_scrap, stock_size, stock_color, stock_modified_date) values (@stockName,@stockDesc, @stockQty, @stockRP, " +
                "@stockUsed, @stockScrap ,@stockSize, @stockColor, @stockModifiedDate);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockName",
                        DataType = DbType.String,
                        Value = stocks.stock_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockDesc",
                        DataType = DbType.String,
                        Value = stocks.stock_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockQty",
                        DataType = DbType.Int16,
                        Value = stocks.stock_quantity
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockRP",
                        DataType = DbType.Int16,
                        Value = stocks.stock_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockUsed",
                        DataType = DbType.Int16,
                        Value = stocks.stock_used
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockScrap",
                        DataType = DbType.Int16,
                        Value = stocks.stock_scrap
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockSize",
                        DataType = DbType.String,
                        Value = stocks.stock_size
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockColor",
                        DataType = DbType.String,
                        Value = stocks.stock_color
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockModifiedDate",
                        DataType = DbType.DateTime,
                        Value = stocks.stock_modified_date
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Stocks stocks)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Purchasing.stocks WHERE stock_id=@stockId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockId",
                        DataType = DbType.Int32,
                        Value = stocks.stock_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
