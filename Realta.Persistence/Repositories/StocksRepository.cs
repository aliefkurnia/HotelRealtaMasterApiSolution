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
                "stock_name=@stockName, stock_description=@stockDesc, " +
                "stock_quantity=@stockQty, stock_reorder_point=@stockRP, " +
                "stock_used=@stockUsed, stock_scrap=@stockScrap, " +
                "stock_size=@stockSize, stock_color=@stockColor, " +
                "stock_modified_date=@stockModifiedDate " +
                "WHERE stock_id=@stockId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockId",
                        DataType = DbType.Int32,
                        Value = stocks.StockId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockName",
                        DataType = DbType.String,
                        Value = stocks.StockName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockDesc",
                        DataType = DbType.String,
                        Value = stocks.StockDesc
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockQty",
                        DataType = DbType.Int16,
                        Value = stocks.StockQty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockRP",
                        DataType = DbType.Int16,
                        Value = stocks.StockReorderPoint
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockUsed",
                        DataType = DbType.Int16,
                        Value = stocks.StockUsed
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockScrap",
                        DataType = DbType.Int16,
                        Value = stocks.StockScrap
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockSize",
                        DataType = DbType.String,
                        Value = stocks.StockSize
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockColor",
                        DataType = DbType.String,
                        Value = stocks.StockColor
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockModifiedDate",
                        DataType = DbType.DateTime,
                        Value = stocks.StockModifiedDate
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Stocks> FindAllStocks()
        {
            IEnumerator<Stocks> dataSet = FindAll<Stocks>("SELECT stock_id as StockId, stock_name as StockName, " +
                "stock_description as StockDesc, stock_quantity as StockQty, stock_reorder_point as StockReorderPoint, " +
                "stock_used as StockUsed, stock_scrap as StockScrap, stock_size as StockSize, stock_color as StockColor, " +
                "stock_modified_date as StockModifiedDate FROM Purchasing.stocks");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;

            }

            //var dataSet = GetAll<Stocks>("SELECT * FROM Purchasing.stocks");

            //return dataSet;
        }

        public async Task<IEnumerable<Stocks>> FindAllStocksAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT stock_id as StockId, stock_name as StockName, " +
                "stock_description as StockDesc, stock_quantity as StockQty, stock_reorder_point as StockReorderPoint, " +
                "stock_used as StockUsed, stock_scrap as StockScrap, stock_size as StockSize, stock_color as StockColor, " +
                "stock_modified_date as StockModifiedDate FROM Purchasing.stocks",
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
                CommandText = "SELECT stock_id as StockId, stock_name as StockName, " +
                "stock_description as StockDesc, stock_quantity as StockQty, stock_reorder_point as StockReorderPoint, " +
                "stock_used as StockUsed, stock_scrap as StockScrap, stock_size as StockSize, stock_color as StockColor, " +
                "stock_modified_date as StockModifiedDate FROM Purchasing.stocks where stock_id=@stockId;",
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
                CommandText = "INSERT INTO purchasing.stocks (stock_name, stock_description, " +
                "stock_quantity, stock_reorder_point, stock_used, " +
                "stock_scrap, stock_size, stock_color, " +
                "stock_modified_date) values (@stockName,@stockDesc, @stockQty, @stockRP, " +
                "@stockUsed, @stockScrap ,@stockSize, @stockColor, @stockModifiedDate);" +
                "SELECT CAST (scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockName",
                        DataType = DbType.String,
                        Value = stocks.StockName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockDesc",
                        DataType = DbType.String,
                        Value = stocks.StockDesc
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockQty",
                        DataType = DbType.Int16,
                        Value = stocks.StockQty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockRP",
                        DataType = DbType.Int16,
                        Value = stocks.StockReorderPoint
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockUsed",
                        DataType = DbType.Int16,
                        Value = stocks.StockUsed
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockScrap",
                        DataType = DbType.Int16,
                        Value = stocks.StockScrap
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockSize",
                        DataType = DbType.String,
                        Value = stocks.StockSize
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockColor",
                        DataType = DbType.String,
                        Value = stocks.StockColor
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@stockModifiedDate",
                        DataType = DbType.DateTime,
                        Value = stocks.StockModifiedDate
                    }
                }
            };

            stocks.StockId = _adoContext.ExecuteScalar<int>(model);
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
                        Value = stocks.StockId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
