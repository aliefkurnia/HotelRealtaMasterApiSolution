using Realta.Domain.Base;
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
                CommandText = "UPDATE purchasing.purchase_order_header SET pohe_status=@PoheStatus WHERE pohe_number= @PoheNumber;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PoheNumber",
                        DataType = DbType.String,
                        Value = purchaseOrderHeader.pohe_number
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
                CommandText = "SELECT * FROM purchasing.purchase_order_header AS pohe JOIN purchasing.purchase_order_detail AS pode ON pohe.pohe_id = pode.pode_pohe_id;",
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

        public PurchaseOrderHeader FindByPo(string po)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT * FROM purchasing.purchase_order_header where pohe_number=@poheNumber;",
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
                CommandText = "INSERT INTO purchasing.purchase_order_header (pohe_number, pohe_tax, pohe_pay_type, pohe_refund, pohe_emp_id, pohe_vendor_id) values (@poheNumber, @poheTax, @pohePayType, @poheRefund, @poheEmpId, @poheVendorId); SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
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
                CommandText = "DELETE FROM purchasing.purchase_order_detail WHERE pode_pohe_id IN (SELECT pohe_id FROM purchasing.purchase_order_header WHERE pohe_number = @poheNumber); DELETE FROM purchasing.purchase_order_header WHERE pohe_number = @poheNumber;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheNumber",
                        DataType = DbType.String,
                        Value = purchaseOrderHeader.pohe_number
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public PurchaseOrderHeader GetLastPoByDate(DateTime date)
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT TOP 1 pohe_number FROM purchasing.purchase_order_header WHERE pohe_number LIKE @poheNumber ORDER BY pohe_number DESC;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@poheNumber",
                        DataType = DbType.String,
                        Value = $"PO-{date:yyyyMMdd}-%"
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
    }
}
