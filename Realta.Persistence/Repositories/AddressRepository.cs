using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Address address)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.address SET addr_line1=@addr_line1,addr_line2=@addr_line2,addr_postal_code=@addr_postal_code,addr_spatial_location=@addr_spatial_location, addr_prov_id= @addr_prov_id  WHERE addr_id= @addr_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_id",
                        DataType = DbType.Int32,
                        Value = address.addr_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_line1",
                        DataType = DbType.String,
                        Value = address.addr_line1
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_line2",
                        DataType = DbType.String,
                        Value = address.addr_line2
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_postal_code",
                        DataType = DbType.String,
                        Value = address.addr_postal_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_spatial_location",
                        DataType = DbType.String,
                        Value = address.addr_spatial_location
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_prov_id",
                        DataType = DbType.Int32,
                        Value = address.addr_prov_id
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public Address FindAddressById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.address where addr_id=@addr_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Address>(model);

            Address? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public IEnumerable<Address> FindAllAddress()
        {
            IEnumerator<Address> dataset = FindAll<Address>("SELECT * from master.address ORDER BY addr_id;");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Address>> FindAllAddressAsync()
        {
            throw new NotImplementedException();
        }

        public void Insert(Address address)
        {
            SqlCommandModel model = new SqlCommandModel()
            {

            
                 CommandText = "INSERT INTO master.address (addr_line1,addr_line2,addr_postal_code,addr_spatial_location, addr_prov_id) VALUES (@addr_line1,@addr_line2,@addr_postal_code,@addr_spatial_location, @addr_prov_id);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_id",
                        DataType = DbType.Int32,
                        Value = address.addr_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_line1",
                        DataType = DbType.String,
                        Value = address.addr_line1
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_line2",
                        DataType = DbType.String,
                        Value = address.addr_line2
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_postal_code",
                        DataType = DbType.String,
                        Value = address.addr_postal_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_spatial_location",
                        DataType = DbType.String,
                        Value = address.addr_spatial_location
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@addr_prov_id",
                        DataType = DbType.Int32,
                        Value = address.addr_prov_id
                    }
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            address.addr_id = _adoContext.ExecuteScalar<int>(model);
                _adoContext.Dispose();
        }

        public void Remove(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
