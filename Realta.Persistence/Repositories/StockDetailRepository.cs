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

        public IEnumerable<StockDetail> FindAllStockDetail()
        {
            IEnumerator<StockDetail> dataSet = FindAll<StockDetail>("SELECT stod_id as StodId, stod_stock_Id as StodStockId, " +
                "stod_barcode_number as StodBarcodeNumber, stod_status as StodStatus, stod_notes as StodNotes, " +
                "stod_faci_id as StodFaciId, stod_pohe_id as StodPoheId FROM Purchasing.stock_detail");

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
                CommandText = "SELECT stod_id as StodId, stod_stock_Id as StodStockId, " +
                "stod_barcode_number as StodBarcodeNumber, stod_status as StodStatus, stod_notes as StodNotes, " +
                "stod_faci_id as StodFaciId, stod_pohe_id as StodPoheId FROM Purchasing.stock_detail",
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
                CommandText = "SELECT stod_id as StodId, stod_stock_Id as StodStockId, " +
                "stod_barcode_number as StodBarcodeNumber, stod_status as StodStatus, stod_notes as StodNotes, " +
                "stod_faci_id as StodFaciId, stod_pohe_id as StodPoheId FROM Purchasing.stock_detail " +
                "where stod_id=@stodId;",
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
                        Value = stockDetail.StodId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void SwitchStatus(StockDetail stockDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "Purchasing.spUpdateStockDetail @stodStockId, @stodStatus, @stodNotes, @stodFaciId, @stodId",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodStockId",
                        DataType = DbType.Int32,
                        Value = stockDetail.StodStockId
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodStatus",
                        DataType = DbType.String,
                        Value = stockDetail.StodStatus
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodNotes",
                        DataType = DbType.String,
                        Value = stockDetail.StodNotes
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodFaciId",
                        DataType = DbType.Int32,
                        Value = stockDetail.StodFaciId
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodId",
                        DataType = DbType.Int32,
                        Value = stockDetail.StodId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
