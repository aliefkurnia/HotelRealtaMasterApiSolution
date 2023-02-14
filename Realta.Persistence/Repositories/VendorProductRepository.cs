using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class VendorProductRepository : RepositoryBase<VendorProduct>, IVendorProductRepository
    {
        public VendorProductRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(VendorProduct venPro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VendorProduct> FindAllVendorProduct()
        {
            IEnumerator<VendorProduct> dataSet = FindAll<VendorProduct>("Select * From purchasing.vendor_product");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public  async Task<IEnumerable<VendorProduct>> FindAllVendorProductAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM purchasing.vendor_product;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };

            IAsyncEnumerator<VendorProduct > dataSet = FindAllAsync<VendorProduct>(model);

            var result = new List<VendorProduct>();
            while (await dataSet.MoveNextAsync())
            {
                result.Add(dataSet.Current);
            }


            return result;
        }

        public VendorProduct FindVendorProductById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM purchasing.vendor_product where vepro_id=@veproId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@veproId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<VendorProduct>(model);
            VendorProduct? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(VendorProduct venPro)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO purchasing.vendor_product (vepro_qty_stocked, vepro_qty_remaining, vepro_price, " +
                    "venpro_stock_id, vepro_vendor_id) " +
                    "VALUES (@vepro_qty_stocked, @vepro_qty_remaining, @vepro_price, @venpro_stock_id, @vepro_vendor_id); " +
                    "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_qty_stocked",
                        DataType = DbType.Int32,
                        Value = venPro.vepro_qty_stocked
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_qty_remaining",
                        DataType = DbType.Int32,
                        Value = venPro.vepro_qty_remaining
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_price",
                        DataType = DbType.Decimal,
                        Value = venPro.vepro_price
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@venpro_stock_id",
                        DataType = DbType.Int32,
                        Value = venPro.venpro_stock_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_vendor_id",
                        DataType = DbType.Int32,
                        Value = venPro.vepro_vendor_id
                    },
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            venPro.vepro_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(VendorProduct venPro)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "Delete from purchasing.vendor_product WHERE vepro_id = @id",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                   new SqlCommandParameterModel()
                   {
                       ParameterName = "@id",
                       DataType = DbType.Int32,
                       Value = venPro.vepro_id
                   }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
