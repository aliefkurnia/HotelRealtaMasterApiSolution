using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class RegionsRepository : RepositoryBase<Regions>, IRegionsRepository

    {
        public RegionsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Regions regions)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.regions SET region_name=@region_name WHERE region_code= @region_code;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@region_code",
                        DataType = DbType.Int32,
                        Value = regions.region_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@region_name",
                        DataType = DbType.String,
                        Value = regions.region_name
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Regions> FindAllRegions()
        {
            IEnumerator<Regions> dataset = FindAll<Regions>("SELECT * FROM master.regions ORDER BY region_code;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Regions>> FindAllRegionsAsync()
        {
            throw new NotImplementedException();
        }


        public Regions FindRegionsById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.regions where region_code=@region_code;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@region_code",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Regions>(model);

            Regions? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;

        }

        public void Insert(Regions regions)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.regions (Region_name) values (@Region_name);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@region_code",
                        DataType = DbType.Int32,
                        Value = regions.region_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@Region_name",
                        DataType = DbType.String,
                        Value = regions.region_name
                    }
                }
            };
            regions.region_code = _adoContext.ExecuteScalar<int>(model);
            //_adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Regions regions)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.regions WHERE region_code=@region_code;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@region_code",
                        DataType = DbType.Int32,
                        Value = regions.region_code
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
