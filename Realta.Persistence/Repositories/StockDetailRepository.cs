using Microsoft.VisualBasic;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
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
    internal class StockDetailRepository : RepositoryBase<StockDetail>, IStockDetailRepository
    {
        public StockDetailRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(StockDetail stockDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE purchasing.stock_detail SET " +
                "stod_stock_id=@stodStockId, stod_barcode_number=@stodBarcodeNumber, stod_status=@stodStatus, " +
                "stod_notes=@stodNotes, stod_faci_id=@stodFaciId, stod_pohe_id=@stodPoheId" +
                " WHERE stod_id=@stodId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_id
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodStockId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_stock_id
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodBarcodeNumber",
                        DataType = DbType.String,
                        Value = stockDetail.stod_barcode_number
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodStatus",
                        DataType = DbType.String,
                        Value = stockDetail.stod_status
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodNotes",
                        DataType = DbType.String,
                        Value = stockDetail.stod_notes
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodFaciId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_faci_id
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodPoheId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_pohe_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<StockDetail> FindAllStockDetail()
        {
            IEnumerator<StockDetail> dataSet = FindAll<StockDetail>("SELECT * FROM Purchasing.stock_detail");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;

            }
        }

        public async Task<IEnumerable<StockDetail>> FindAllStockDetailAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Purchasing.stock_detail;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<StockDetail> dataSet = FindAllAsync<StockDetail>(model);

            var item = new List<StockDetail>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public StockDetail FindStockDetailById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM purchasing.stock_detail where stod_id=@stodId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stodId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<StockDetail>(model);

            StockDetail? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(StockDetail stockDetail)
        {

            SqlCommandModel model = new SqlCommandModel()
            {
                
                CommandText = "INSERT INTO purchasing.stock_detail (stod_stock_id, stod_barcode_number, stod_status, stod_notes, stod_faci_id, " +
                "stod_pohe_id) values (@stodStockId,@stodBarcodeNumber, @stodStatus, @stodNotes, " +
                "@stodFaciId, @stodPoheId);" +
                "SELECT CAST (scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodStockId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_stock_id
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodBarcodeNumber",
                        DataType = DbType.String,
                        Value = stockDetail.stod_barcode_number
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodStatus",
                        DataType = DbType.String,
                        Value = stockDetail.stod_status
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodNotes",
                        DataType = DbType.String,
                        Value = stockDetail.stod_notes
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodFaciId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_faci_id
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodPoheId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_pohe_id
                    }
                }
            };

            stockDetail.stod_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(StockDetail stockDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM purchasing.stock_detail where stod_id=@stodId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stodId",
                        DataType = DbType.Int32,
                        Value = stockDetail.stod_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
