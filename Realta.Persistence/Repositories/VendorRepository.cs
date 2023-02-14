using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.Interface;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(AdoDbContext AdoContext) : base (AdoContext) 
        { 
        }

        public void Edit(Vendor vendor)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE purchasing.vendor SET vendor_name=@vendorName, vendor_active=@vendorActive, vendor_priority=@vendorPriority, " +
                "vendor_weburl=@vendorWebURL WHERE vendor_entity_id = @vendorId;",
                CommandType = CommandType.Text,

                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorId",
                        DataType = DbType.Int32,
                        Value = vendor.vendor_entity_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorName",
                        DataType = DbType.String,
                        Value = vendor.vendor_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorActive",
                        DataType = DbType.Boolean,
                        Value = vendor.vendor_active
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorPriority",
                        DataType = DbType.Boolean,
                        Value = vendor.vendor_priority
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorWebURL",
                        DataType = DbType.String,
                        Value = vendor.vendor_weburl
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Vendor> FindAllVendor()
        {
            IEnumerator<Vendor> dataSet = FindAll<Vendor>("Select * From purchasing.vendor");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Vendor>> FindAllVendorAsync()
        {
            throw new NotImplementedException();
        }

        public Vendor FindVendorById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM purchasing.vendor where vendor_entity_id=@vendorId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Vendor>(model);
            Vendor? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }

            return item;
        
    }

        public void Insert(Vendor vendor)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO purchasing.vendor (vendor_entity_id, vendor_name, vendor_active, vendor_priority, vendor_weburl) " +
                "VALUES (@vendor_entity_id, @vendor_name, @vendor_active, @vendor_priority, @vendor_weburl);;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_entity_id",
                        DataType = DbType.Int32,
                        Value = vendor.vendor_entity_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_name",
                        DataType = DbType.String,
                        Value = vendor.vendor_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_active",
                        DataType = DbType.Boolean,
                        Value = vendor.vendor_active
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_priority",
                        DataType = DbType.Boolean,
                        Value = vendor.vendor_priority
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_weburl",
                        DataType = DbType.String,
                        Value = vendor.vendor_weburl
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Vendor vendor)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM purchasing.vendor WHERE vendor_entity_id=@vendorId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendorId",
                        DataType = DbType.Int32,
                        Value = vendor.vendor_entity_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
