using Microsoft.VisualBasic;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
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

        public async Task<IEnumerable<StockDetail>> FindAllStockDetailByStockId(int stockId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
               CommandText = "SELECT stod_id as StodId, stod_stock_id as StodStockId, stod_barcode_number as StodBarcodeNumber, " +
               "stod_status as StodStatus, stod_notes as StodNotes, stod_faci_id as StodFaciId, " +
               "stod_pohe_id as StodPoheId FROM Purchasing.stock_detail " +
               "where stod_stock_id=@stodStockId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stodStockId",
                        DataType = DbType.Int32,
                        Value = stockId
                    }
                }
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

        public void GenerateBarcodePO(PurchaseOrderDetail purchaseOrderDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "Purchasing.GenerateBarcode  @PodeId, @PodeQyt, @PodeReceivedQty, @PodeRejectQty",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   new SqlCommandParameterModel() {
                        ParameterName = "@PodeId",
                        DataType = DbType.Int16,
                        Value = purchaseOrderDetail.PodeId
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@PodeQyt",
                        DataType = DbType.String,
                        Value = purchaseOrderDetail.PodeOrderQty
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@PodeReceivedQty",
                        DataType = DbType.String,
                        Value = purchaseOrderDetail.PodeReceivedQty
                    },
                   new SqlCommandParameterModel() {
                        ParameterName = "@PodeRejectQty",
                        DataType = DbType.Int16,
                        Value = purchaseOrderDetail.PodeRejectedQty
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
                CommandText = "Purchasing.spUpdateStockDetail  @stodId, @stodStatus, @stodNotes, @stodFaciId",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                   new SqlCommandParameterModel() {
                        ParameterName = "@stodId",
                        DataType = DbType.Int16,
                        Value = stockDetail.StodId
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
                        DataType = DbType.Int16,
                        Value = stockDetail.StodFaciId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
