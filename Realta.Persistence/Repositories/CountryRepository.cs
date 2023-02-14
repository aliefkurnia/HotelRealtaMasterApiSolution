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
    internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Country country)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.Country SET country_name=@country_name,country_region_id=@country_region_id WHERE country_id= @country_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_id",
                        DataType = DbType.Int32,
                        Value = country.country_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_name",
                        DataType = DbType.String,
                        Value = country.country_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_region_id",
                        DataType = DbType.Int32,
                        Value = country.country_region_id
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();

        }

        public IEnumerable<Country> FindAllCountry()
        {
            IEnumerator<Country> dataset = FindAll<Country>("SELECT * FROM master.country ORDER BY country_id;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Country>> FindAllCountryAsync()
        {
            throw new NotImplementedException();
        }

        public Country FindCountryById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.country where country_id=@country_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Country>(model);

            Country? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }


        public void Insert(Country country)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.country (country_name,country_region_id) values (@country_name,@country_region_id);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_name",
                        DataType = DbType.String,
                        Value = country.country_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_region_id",
                        DataType = DbType.String,
                        Value = country.country_region_id
                    }
                }
            };
            country.country_id = _adoContext.ExecuteScalar<int>(model);
            //_adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Country country)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.country WHERE country_id=@country_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@country_id",
                        DataType = DbType.Int32,
                        Value = country.country_id
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
