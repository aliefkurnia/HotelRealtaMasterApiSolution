using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class StockPhotoRepository : RepositoryBase<StockPhoto>, IStockPhotoRepository
    {
        public StockPhotoRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public StockPhoto FindStockPhotoById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM purchasing.stock_photo where spho_id=@sphoId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@sphoId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<StockPhoto>(model);

            StockPhoto? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void InsertUploadPhoto(StockPhoto stockPhoto)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO purchasing.stock_photo (spho_photo_filename, spho_primary, " +
                "spho_stock_id) values (@sphoPhotoFilename, @sphoPrimary, " +
                "@sphoStockId);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@sphoPhotoFilename",
                        DataType = DbType.String,
                        Value = stockPhoto.SphoPhotoFileName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@sphoPrimary",
                        DataType = DbType.Int16,
                        Value = stockPhoto.SphoPrimary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@sphoStockId",
                        DataType = DbType.Int32,
                        Value = stockPhoto.SphoStockId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(StockPhoto stockPhoto)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Purchasing.stock_photo WHERE spho_id=@sphoId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@sphoId",
                        DataType = DbType.Int32,
                        Value = stockPhoto.SphoId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}
