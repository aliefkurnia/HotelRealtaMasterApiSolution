using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
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

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE purchasing.vendor_product SET vepro_qty_stocked = @vepro_qty_stocked, vepro_qty_remaining = @vepro_qty_remaining, " +
                    "vepro_price = @vepro_price " +
                    "WHERE vepro_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = venPro.VeproId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_qty_stocked",
                        DataType = DbType.Int32,
                        Value = venPro.VeproQtyStocked
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_qty_remaining",
                        DataType = DbType.Int32,
                        Value = venPro.VeproQtyRemaining
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_price",
                        DataType = DbType.Decimal,
                        Value = venPro.VeproPrice
                    },
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public  async Task<IEnumerable<VendorProduct>> FindAllVendorProductAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText ="SELECT venpro.vepro_id as VeproId, " +
                "ven.vendor_name as VendorName, "+
                "s.stock_name as StockName, "+
                "venpro.vepro_qty_stocked as VeproQtyStocked, "+
                "venpro.vepro_qty_remaining  as VeproQtyRemaining, "+
                "venpro.vepro_price as VeproPrice, "+
				"venpro.venpro_stock_id as VenproStockId, "+
				"venpro.vepro_vendor_id as VeproVendorId "+
                "FROM Purchasing.vendor_product as venpro "+
                "JOIN Purchasing.stocks as s "+
                "ON s.stock_id = venpro.venpro_stock_id "+
                "JOIN Purchasing.vendor as ven "+
                "ON venpro.vepro_vendor_id = ven.vendor_entity_id;",
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
                CommandText =
                "SELECT venpro.vepro_id as VeproId, " +
                "ven.vendor_name as VendorName, " +
                "s.stock_name as StockName, "+
                "venpro.vepro_qty_stocked as VeproQtyStocked, "+
                "venpro.vepro_qty_remaining as VeproQtyRemaining, "+
                "venpro.vepro_price as VeproPrice, " +
                "venpro.venpro_stock_id as VenproStockId, " +
                "venpro.vepro_vendor_id as VeproVendorId " +
                "FROM Purchasing.vendor_product as venpro "+
                "JOIN Purchasing.stocks as s "+
                "ON s.stock_id = venpro.venpro_stock_id "+
                "JOIN Purchasing.vendor as ven "+
                "ON venpro.vepro_vendor_id = ven.vendor_entity_id "+
                "WHERE venpro.vepro_id = @Id; "
                ,
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@Id",
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
                        Value = venPro.VeproQtyStocked
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_qty_remaining",
                        DataType = DbType.Int32,
                        Value = venPro.VeproQtyRemaining
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_price",
                        DataType = DbType.Decimal,
                        Value = venPro.VeproPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@venpro_stock_id",
                        DataType = DbType.Int32,
                        Value = venPro.VenproStockId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_vendor_id",
                        DataType = DbType.Int32,
                        Value = venPro.VeproVendorId
                    },
                }
            };
            venPro.VeproId = _adoContext.ExecuteScalar<int>(model);
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
                       Value = venPro.VeproId
                   }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
        public bool ValidasiInsert (int stockId, int vendorId)
        {

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText =
                "SELECT venpro.vepro_id as VeproId, "+
                "venpro.vepro_qty_stocked as VeproQtyStocked, "+
                "venpro.vepro_qty_remaining as VeproQtyRemaining, "+
                "venpro.vepro_price as VeproPrice, "+
                "venpro.venpro_stock_id as VenproStockId, "+
                "venpro.vepro_vendor_id as VeproVendorId "+
                "FROM Purchasing.vendor_product as venpro "+
                "WHERE venpro_stock_id = @venpro_stock_id AND vepro_vendor_id = @vepro_vendor_id; ",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@venpro_stock_id",
                        DataType = DbType.Int32,
                        Value = stockId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vepro_vendor_id",
                        DataType = DbType.Int32,
                        Value = vendorId
                    }
                }
            };

            var dataSet = FindByCondition<VendorProduct>(model);
            VendorProduct? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return (item==null) ? true: false;
        }
        public async Task<IEnumerable<VendorProduct>> FindVendorProductByVendorId(int vendorId)
        {
            string sqlStatement =
              "SELECT venpro.vepro_id as VeproId, " +
              "ven.vendor_name as VendorName, " +
              "s.stock_name as StockName, " +
              "venpro.vepro_qty_stocked as VeproQtyStocked, " +
              "venpro.vepro_qty_remaining as VeproQtyRemaining, " +
              "venpro.vepro_price as VeproPrice, " +
              "venpro.venpro_stock_id as VenproStockId, " +
              "venpro.vepro_vendor_id as VeproVendorId " +
              "FROM Purchasing.vendor_product as venpro " +
              "JOIN Purchasing.stocks as s " +
              "ON s.stock_id = venpro.venpro_stock_id " +
              "JOIN Purchasing.vendor as ven " +
              "ON venpro.vepro_vendor_id = ven.vendor_entity_id " +
              "WHERE ven.vendor_entity_id = @Id; ";

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = sqlStatement,
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@Id",
                        DataType = DbType.Int32,
                        Value = vendorId
                    }
                }
            };
            IAsyncEnumerator<VendorProduct> dataSet = FindAllAsync<VendorProduct>(model);
            var item = new List<VendorProduct>();
            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }
            return item;
        }

    }
}
