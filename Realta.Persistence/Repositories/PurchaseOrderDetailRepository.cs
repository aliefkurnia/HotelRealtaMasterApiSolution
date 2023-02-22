using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System.Data;

namespace Realta.Persistence.Repositories
{
    internal class PurchaseOrderDetailRepository : RepositoryBase<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepository(AdoDbContext AdoContext) : base(AdoContext)
        {
        }

        public void UpdateQty(PurchaseOrderDetail purchaseOrderDetail)
        {
            SqlCommandModel model = new()
            {
                CommandText = "UPDATE purchasing.purchase_order_detail SET pode_order_qty = @PodeOrderQty, pode_received_qty = @PodeReceivedQty, pode_rejected_qty = @PodeRejectedQty WHERE pode_id= @PodeId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeOrderQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_order_qty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeReceivedQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_received_qty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeRejectedQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_rejected_qty
                    }      
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<PurchaseOrderDetail> FindAll()
        {
            IEnumerator<PurchaseOrderDetail> dataSet = FindAll<PurchaseOrderDetail>("SELECT * FROM purchasing.purchase_order_detail;");
            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> FindAllAsync(string poNumber)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT pod.* FROM purchasing.purchase_order_detail AS pod JOIN purchasing.purchase_order_header poh ON poh.pohe_id = pod.pode_pohe_id where poh.pohe_number = @poheNumber;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheNumber",
                        DataType = DbType.String,
                        Value = poNumber
                    }
                }

            };
            IAsyncEnumerator<PurchaseOrderDetail> dataSet = FindAllAsync<PurchaseOrderDetail>(model);
            var item = new List<PurchaseOrderDetail>();
            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }
            return item;
        }

        public PurchaseOrderDetail FindById(int id)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT * FROM purchasing.purchase_order_detail where pode_id=@podeId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@podeId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<PurchaseOrderDetail>(model);

            PurchaseOrderDetail? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(PurchaseOrderDetail purchaseOrderDetail)
        {
            SqlCommandModel model = new()
            {
                CommandText = "INSERT INTO purchasing.purchase_order_detail (pode_pohe_id, pode_order_qty, pode_price, pode_stock_id, pode_received_qty, pode_rejected_qty) VALUES (@PodePoheId, @PodeOrderQty, @PodePrice, @PodeStockId, @PodeReceivedQty, @PodeRejectedQty); SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodePoheId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_pohe_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeOrderQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_order_qty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodePrice",
                        DataType = DbType.Decimal,
                        Value = purchaseOrderDetail.pode_price
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeStockId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_stock_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeReceivedQty",
                        DataType = DbType.Decimal,
                        Value = purchaseOrderDetail.pode_received_qty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeRejectedQty",
                        DataType = DbType.Decimal,
                        Value = purchaseOrderDetail.pode_rejected_qty
                    }

                }
            };

            //_adoContext.ExecuteNonQuery(model);
            purchaseOrderDetail.pode_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(PurchaseOrderDetail purchaseOrderDetail)
        {
            SqlCommandModel model = new()
            {
                CommandText = "DELETE FROM purchasing.purchase_order_detail WHERE pode_id=@podeId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@podeId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.pode_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
