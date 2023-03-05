using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System.Data;
using System.Data.SqlClient;

namespace Realta.Persistence.Repositories
{
    internal class PurchaseOrderRepository : RepositoryBase<PurchaseOrderHeader>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(AdoDbContext AdoContext) : base(AdoContext)
        {
        }

        public void UpdateStatus(PurchaseOrderHeader header)
        {
            SqlCommandModel model = new()
            {
                CommandText = "UPDATE purchasing.purchase_order_header SET pohe_status=@PoheStatus WHERE pohe_number=@PoheNumber;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PoheNumber",
                        DataType = DbType.String,
                        Value = header.PoheNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PoheStatus",
                        DataType = DbType.String,
                        Value = header.PoheStatus
                    }
                }
            };

            Update(model);
        }

        public void UpdateQty(PurchaseOrderDetail purchaseOrderDetail)
        {
            SqlCommandModel model = new()
            {
                CommandText = "UPDATE purchasing.purchase_order_detail " +
                                "SET pode_order_qty = @PodeOrderQty, " +
                                "pode_received_qty = @PodeReceivedQty, " +
                                "pode_rejected_qty = @PodeRejectedQty " +
                                "WHERE pode_id= @PodeId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.PodeId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeOrderQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.PodeOrderQty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeReceivedQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.PodeReceivedQty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PodeRejectedQty",
                        DataType = DbType.Int32,
                        Value = purchaseOrderDetail.PodeRejectedQty
                    }
                }
            };

            Update(model);
        }

        public async Task<IEnumerable<PurchaseOrderHeader>> FindAllAsync()
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT poh.pohe_id AS PoheId, " +
                                "poh.pohe_number AS PoheNumber, " +
                                "poh.pohe_status AS PoheStatus, " +
                                "poh.pohe_order_date AS PoheOrderDate, " +
                                "poh.pohe_subtotal AS PoheSubtotal, " +
                                "poh.pohe_tax AS PoheTax, " +
                                "poh.pohe_total_amount AS PoheTotalAmount, " +
                                "poh.pohe_refund AS PoheRefund, " +
                                "poh.pohe_arrival_date AS PoheArrivalDate, " +
                                "poh.pohe_pay_type AS PohePayType, " +
                                "ven.vendor_name AS VendorName, " +
                                "poh.pohe_emp_id AS PoheEmpId, " +
                                "poh.pohe_vendor_id AS PoheVendorId " +
                                "FROM purchasing.purchase_order_header AS poh " +
                                "JOIN purchasing.vendor AS ven ON ven.vendor_entity_id = poh.pohe_vendor_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };
            IAsyncEnumerator<PurchaseOrderHeader> dataSet = FindAllAsync<PurchaseOrderHeader>(model);
            var item = new List<PurchaseOrderHeader>();
            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }
            return item;
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> FindAllDetAsync(string po)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT pod.pode_id AS PodeId, " +
                                "sto.stock_name AS StockName, " +
                                "pod.pode_pohe_id AS PodePoheId, " +
                                "pod.pode_order_qty AS PodeOrderQty, " +
                                "pod.pode_price AS PodePrice, " +
                                "pod.pode_line_total AS PodeLineTotal, " +
                                "pod.pode_received_qty AS PodeReceivedQty, " +
                                "pod.pode_rejected_qty AS PodeRejectedQty, " +
                                "pod.pode_stocked_qty AS PodeStockedQty, " +
                                "pod.pode_modified_date AS PodeModifiedDate, " +
                                "pod.pode_stock_id AS PodeStockId " +
                                "FROM purchasing.purchase_order_detail AS pod " +
                                "JOIN purchasing.purchase_order_header AS poh ON poh.pohe_id = pod.pode_pohe_id " +
                                "JOIN purchasing.stocks AS sto ON sto.stock_id = pod.pode_stock_id " +
                                "WHERE poh.pohe_number = @poheNumber;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheNumber",
                        DataType = DbType.String,
                        Value = po
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

        public PurchaseOrderHeader FindByPo(string po)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT pohe_id AS PoheId, " +
                                "pohe_number AS PoheNumber, " +
                                "pohe_status AS PoheStatus, " +
                                "pohe_order_date AS PoheOrderDate, " +
                                "pohe_subtotal AS PoheSubtotal, " +
                                "pohe_tax AS PoheTax, " +
                                "pohe_total_amount AS PoheTotalAmount, " +
                                "pohe_refund AS PoheRefund, " +
                                "pohe_arrival_date AS PoheArrivalDate, " +
                                "pohe_pay_type AS PohePayType, " +
                                "pohe_emp_id AS PoheEmpId, " +
                                "ven.vendor_name AS VendorName, " +
                                "pohe_vendor_id AS PoheVendorId " +
                                "FROM purchasing.purchase_order_header AS poh " +
                                "JOIN purchasing.vendor AS ven ON ven.vendor_entity_id = poh.pohe_vendor_id " +
                                "WHERE pohe_number = @poheNumber;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheNumber",
                        DataType = DbType.String,
                        Value = po
                    }
                }
            };

            var dataSet = FindByCondition<PurchaseOrderHeader>(model);

            PurchaseOrderHeader? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public PurchaseOrderHeader FindById(int id)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT pohe_id AS PoheId, " +
                                "pohe_number AS PoheNumber, " +
                                "pohe_status AS PoheStatus, " +
                                "pohe_order_date AS PoheOrderDate, " +
                                "pohe_subtotal AS PoheSubtotal, " +
                                "pohe_tax AS PoheTax, " +
                                "pohe_total_amount AS PoheTotalAmount, " +
                                "pohe_refund AS PoheRefund, " +
                                "pohe_arrival_date AS PoheArrivalDate, " +
                                "pohe_pay_type AS PohePayType, " +
                                "pohe_emp_id AS PoheEmpId, " +
                                "ven.vendor_name AS VendorName, " +
                                "pohe_vendor_id AS PoheVendorId " +
                                "FROM purchasing.purchase_order_header AS poh " +
                                "JOIN purchasing.vendor AS ven ON ven.vendor_entity_id = pod.pode_vendor_id " +
                                "WHERE pohe_id = @poheId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<PurchaseOrderHeader>(model);

            PurchaseOrderHeader? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public PurchaseOrderDetail FindDetById(int id)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT pode_id AS PodeId, " +
                                "sto.stock_name AS StockName, " +
                                "pode_pohe_id AS PodePoheId, " +
                                "pode_order_qty AS PodeOrderQty, " +
                                "pode_price AS PodePrice, " +
                                "pode_line_total AS PodeLineTotal, " +
                                "pode_received_qty AS PodeReceivedQty, " +
                                "pode_rejected_qty AS PodeRejectedQty, " +
                                "pode_stocked_qty AS PodeStockedQty, " +
                                "pode_modified_date AS PodeModifiedDate, " +
                                "pode_stock_id AS PodeStockId " +
                                "FROM purchasing.purchase_order_detail AS pod " +
                                "JOIN purchasing.stocks AS sto ON sto.stock_id = pod.pode_stock_id " +
                                "WHERE pode_id=@podeId;",
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

        public void Insert(PurchaseOrderHeader header, PurchaseOrderDetail detail)
        {
            SqlCommandModel model = new()
            {
                CommandText = "purchasing.spInsertPurchaseOrder", // nama stored procedure
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@pohe_pay_type",
                        DataType = DbType.String,
                        Value = header.PohePayType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pohe_emp_id",
                        DataType = DbType.Int32,
                        Value = header.PoheEmpId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pohe_vendor_id",
                        DataType = DbType.Int32,
                        Value = header.PoheVendorId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pode_order_qty",
                        DataType = DbType.Int32,
                        Value = detail.PodeOrderQty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pode_price",
                        DataType = DbType.Decimal,
                        Value = detail.PodePrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pode_stock_id",
                        DataType = DbType.Int32,
                        Value = detail.PodeStockId
                    }
                }
            };

            Create(model);
        }

        public void Remove(PurchaseOrderHeader header)
        {
            SqlCommandModel model = new()
            {
                CommandText = "purchasing.spDeletePurchaseOrder;",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@pohe_number",
                        DataType = DbType.String,
                        Value = header.PoheNumber
                    }
                }
            };

            Delete(model);
        }

        public void RemoveDetail(PurchaseOrderDetail detail)
        {
            SqlCommandModel model = new()
            {
                CommandText = "DELETE FROM purchasing.purchase_order_detail WHERE pode_id=@podeId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@podeId",
                        DataType = DbType.Int32,
                        Value = detail.PodeId
                    }
                }
            };

            Delete(model);
        }
    }
}
