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

        public async Task<PagedList<StockDetail>> FindAllStockDetailByStckIdPaging(StockDetailParameters stockDetailParameters)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT stod_id as StodId, stock_name as StockName, " +
               "stod_barcode_number as StodBarcodeNumber, stod_status as StodStatus, stod_notes as StodNotes, " +
               "fa.faci_room_number as FaciRoomNumber, po.pohe_number as PoheNumber FROM Purchasing.stock_detail sd " +
               "JOIN Purchasing.purchase_order_header po ON sd.stod_pohe_id = po.pohe_id " +
               "JOIN Hotel.facilities fa ON sd.stod_faci_id = fa.faci_id " +
               "JOIN Purchasing.stocks s ON s.stock_id = sd.stod_stock_id " +
               "WHERE sd.stod_stock_id=@stodStockId ORDER BY sd.stod_id " +
               "OFFSET @pageNo ROWS FETCH NEXT @pageSize ROWS ONLY",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@stodStockId",
                        DataType = DbType.Int32,
                        Value = stockDetailParameters.StockId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pageNo",
                        DataType = DbType.Int32,
                        Value = stockDetailParameters.PageNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pageSize",
                        DataType = DbType.Int32,
                        Value = stockDetailParameters.PageSize
                    }
                }
            };

            var stockDetails = await GetAllAsync<StockDetail>(model);
            var totalRows = stockDetails.Count();

            return new PagedList<StockDetail>(stockDetails.ToList(), totalRows, stockDetailParameters.PageNumber, stockDetailParameters.PageSize);
        }

        public async Task<IEnumerable<StockDetail>> FindAllStockDetailByStockId(int stockId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
               CommandText = "SELECT stod_id as StodId, stock_name as StockName, " +
               "stod_barcode_number as StodBarcodeNumber, stod_status as StodStatus, stod_notes as StodNotes, " +
               "fa.faci_room_number as FaciRoomNumber, po.pohe_number as PoheNumber FROM Purchasing.stock_detail sd " +
               "JOIN Purchasing.purchase_order_header po ON sd.stod_pohe_id = po.pohe_id " +
               "JOIN Hotel.facilities fa ON sd.stod_faci_id = fa.faci_id " +
               "Join Purchasing.stocks s on s.stock_id = sd.stod_stock_id " +
               "where sd.stod_stock_id=@stodStockId;",
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
                CommandText = "SELECT stod_id as StodId, stock_name as StockName, " +
               "stod_barcode_number as StodBarcodeNumber, stod_status as StodStatus, stod_notes as StodNotes, " +
               "fa.faci_room_number as FaciRoomNumber, po.pohe_number as PoheNumber FROM Purchasing.stock_detail sd " +
               "JOIN Purchasing.purchase_order_header po ON sd.stod_pohe_id = po.pohe_id " +
               "JOIN Hotel.facilities fa ON sd.stod_faci_id = fa.faci_id " +
               "JOIN Purchasing.stocks s ON s.stock_id = sd.stod_stock_id " +
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
