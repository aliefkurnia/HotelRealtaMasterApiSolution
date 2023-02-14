using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System.Data;

namespace Realta.Persistence.Repositories
{
    internal class PurchaseOrderHeaderRepository : RepositoryBase<PurchaseOrderHeader>, IPurchaseOrderHeaderRepository
    {
        public PurchaseOrderHeaderRepository(AdoDbContext AdoContext) : base(AdoContext)
        {
        }

        public void UpdateStatus(PurchaseOrderHeader purchaseOrderHeader)
        {
            SqlCommandModel model = new()
            {
                CommandText = "UPDATE purchasing.purchase_order_header SET pohe_status=@PoheStatus WHERE pohe_id= @PoheId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PoheId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderHeader.pohe_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PoheStatus",
                        DataType = DbType.String,
                        Value = purchaseOrderHeader.pohe_status
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<PurchaseOrderHeader> FindAll()
        {
            IEnumerator<PurchaseOrderHeader> dataSet = FindAll<PurchaseOrderHeader>("SELECT * FROM purchasing.purchase_order_header;");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;

            }
        }

        public async Task<IEnumerable<PurchaseOrderHeader>> FindAllAsync()
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT * FROM purchasing.purchase_order_header;",
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

        public PurchaseOrderHeader FindById(int id)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT * FROM purchasing.purchase_order_header where pohe_id=@poheId;",
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

        public void Insert(PurchaseOrderHeader purchaseOrderHeader)
        {
            SqlCommandModel model = new()
            {
                CommandText = "INSERT INTO purchasing.purchase_order_header (pohe_number, pohe_tax, pohe_pay_type, pohe_refund, pohe_arrival_date, pohe_emp_id, pohe_vendor_id) values (@poheNumber, @poheTax, @pohePayType, @poheRefund, @poheArrivalDate, @poheEmpId, @poheVendorId); SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    //new SqlCommandParameterModel() {
                    //    ParameterName = "@poheStatus",
                    //    DataType = DbType.String,
                    //    Value = purchaseOrderHeader.pohe_status
                    //},
                    //new SqlCommandParameterModel() {
                    //    ParameterName = "@poheSubtotal",
                    //    DataType = DbType.Decimal,
                    //    Value = purchaseOrderHeader.pohe_subtotal
                    //},
                    //new SqlCommandParameterModel() {
                    //    ParameterName = "@poheTotalAmount",
                    //    DataType = DbType.Decimal,
                    //    Value = purchaseOrderHeader.pohe_total_amount
                    //},
                    //new SqlCommandParameterModel() {
                    //    ParameterName = "@poheOrderDate",
                    //    DataType = DbType.DateTime,
                    //    Value = purchaseOrderHeader.pohe_order_date
                    //},
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheNumber",
                        DataType = DbType.String,
                        Value = purchaseOrderHeader.pohe_number
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheTax",
                        DataType = DbType.Decimal,
                        Value = purchaseOrderHeader.pohe_tax
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheRefund",
                        DataType = DbType.Decimal,
                        Value = purchaseOrderHeader.pohe_refund
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheArrivalDate",
                        DataType = DbType.DateTime,
                        Value = purchaseOrderHeader.pohe_arrival_date
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pohePayType",
                        DataType = DbType.String,
                        Value = purchaseOrderHeader.pohe_pay_type
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheEmpId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderHeader.pohe_emp_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheVendorId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderHeader.pohe_vendor_id
                    }
                }
            };

            //_adoContext.ExecuteNonQuery(model);
            purchaseOrderHeader.pohe_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(PurchaseOrderHeader purchaseOrderHeader)
        {
            SqlCommandModel model = new()
            {
                CommandText = "DELETE FROM purchasing.purchase_order_header WHERE pohe_id=@poheId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheId",
                        DataType = DbType.Int32,
                        Value = purchaseOrderHeader.pohe_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
