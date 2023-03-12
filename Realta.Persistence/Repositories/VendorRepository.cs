using Realta.Domain.Dto;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
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
                CommandText = "[Purchasing].[spUpdateVendor]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@Id",
                        DataType = DbType.Int32,
                        Value = vendor.VendorEntityId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = vendor.VendorName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@Active",
                        DataType = DbType.Boolean,
                        Value = vendor.VendorActive
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@Priority",
                        DataType = DbType.Boolean,
                        Value = vendor.VendorPriority
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@WebURL",
                        DataType = DbType.String,
                        Value = vendor.VendorWeburl
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Vendor> FindAllVendor()
        {
            IEnumerator<Vendor> dataSet = FindAll<Vendor>("Select " +
                    "vendor_entity_id AS VendorEntityId, " +
                    "vendor_name AS VendorName, " +
                    "vendor_active AS VendorActive, " +
                    "vendor_priority AS VendorPriority, " +
                    "vendor_register_date AS VendorRegisterDate, " +
                    "vendor_weburl AS VendorWeburl, " +
                    "vendor_modified_date AS VendorModifiedDate " +
                    "From purchasing.vendor");

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
                CommandText= "[Purchasing].[spFindById]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@Id",
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

        public async Task<IEnumerable<Vendor>> GetVendorPaging(VendorParameters vendorParameters)
        {

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"Select 
                    vendor_entity_id AS VendorEntityId, 
                    vendor_name AS VendorName, 
                    vendor_active AS VendorActive, 
                    vendor_priority AS VendorPriority, 
                    vendor_register_date AS VendorRegisterDate, 
                    vendor_weburl AS VendorWeburl,
                    vendor_modified_date AS VendorModifiedDate 
                    From purchasing.vendor
					ORDER BY VendorEntityId"
                        ,
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                            ParameterName = "@pageNo",
                            DataType = DbType.Int32,
                            Value = vendorParameters.PageNumber
                        },
                     new SqlCommandParameterModel() {
                            ParameterName = "@pageSize",
                            DataType = DbType.Int32,
                            Value = vendorParameters.PageSize
                        }
                }
            };
            var  dataSet = await GetAllAsync<Vendor>(model);

            if (vendorParameters.Keyword != null)
            {
                string decodedKeyword = Uri.UnescapeDataString(vendorParameters.Keyword);
                dataSet = dataSet.Where(p =>
                    p.VendorName.ToLower().Contains(decodedKeyword.ToLower())  );
            }
            var totalRows = dataSet.Count();

            return PagedList<Vendor>.ToPagedList(dataSet.ToList(), vendorParameters.PageNumber, vendorParameters.PageSize);
        }

        public void Insert(Vendor vendor)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO purchasing.vendor (vendor_name, vendor_active, vendor_priority, vendor_weburl) " +
                "VALUES (@vendor_name, @vendor_active, @vendor_priority, @vendor_weburl);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_name",
                        DataType = DbType.String,
                        Value = vendor.VendorName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_active",
                        DataType = DbType.Boolean,
                        Value = vendor.VendorActive
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_priority",
                        DataType = DbType.Boolean,
                        Value = vendor.VendorPriority
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@vendor_weburl",
                        DataType = DbType.String,
                        Value = vendor.VendorWeburl
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
                CommandText = "[Purchasing].[spDeleteVendor]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = vendor.VendorEntityId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
