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
    internal class PriceItemsPhotoRepository : RepositoryBase<PriceItemsPhoto>, IPriceItemsPhotoRepository
    {
        public PriceItemsPhotoRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }



        public void Edit(PriceItemsPhoto priceItemsPhoto)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PriceItemsPhoto> FindAllPriceItemsPhoto()
        {
            IEnumerator<PriceItemsPhoto> dataSet = FindAll<PriceItemsPhoto>("SELECT * FROM Master.Price_ItemsPhotos");

            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;

            }
        }


        public async Task<IEnumerable<PriceItemsPhoto>> FindAllPriceItemsPhotoAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Master.Price_ItemsPhotos;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<PriceItemsPhoto> dataSet = FindAllAsync<PriceItemsPhoto>(model);

            var item = new List<PriceItemsPhoto>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;

        }


        public PriceItemsPhoto FindOrderById(int id)
        {
            throw new NotImplementedException();
        }





        public PriceItems FindPriceItemsPhotoById(int pritId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.price_itemsphotos where prit_id=@prit_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prit_id",
                        DataType = DbType.Int32,
                        Value = pritId
                    }
                }
            };

            var dataSet = FindByCondition<PriceItems>(model);

            var item = new PriceItems();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;

        }





        public void Insert(PriceItemsPhoto priceItemsPhoto)
        {
                SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO Price_ItemsPhotos 
                (PhotoFilename,PhotoFileSize,PhotoFileType,PhotoPrice_ItemsId,PhotoPrimary,PhotoOriginalFilename) 
                values (@PhotoFilename,@PhotoFileSize,@PhotoFileType,@PhotoPrice_ItemsId,@PhotoPrimary,@PhotoOriginalFilename);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFilename",
                        DataType = DbType.String,
                        Value = priceItemsPhoto.PhotoFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFileSize",
                        DataType = DbType.Int64,
                        Value = priceItemsPhoto.PhotoFileSize
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFileType",
                        DataType = DbType.String,
                        Value = priceItemsPhoto.PhotoFileType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoPrice_ItemsId",
                        DataType = DbType.Int64,
                        Value = priceItemsPhoto.PhotoPriceItemsId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoPrimary",
                        DataType = DbType.Int64,
                        Value = priceItemsPhoto.PhotoPrimary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoOriginalFilename",
                        DataType = DbType.String,
                        Value = priceItemsPhoto.PhotoOriginalFilename
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }



        public void Remove(PriceItemsPhoto priceItemsPhoto)
        {
            throw new NotImplementedException();
        }
    }
}
