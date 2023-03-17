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
    internal class CategoryGroupPhotoRepository : RepositoryBase<CategoryGroupPhoto>, ICategoryGroupPhotoRepository
    {
        public CategoryGroupPhotoRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }



        public void Edit(CategoryGroupPhoto categoryGroupPhoto)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<CategoryGroupPhoto> FindAllCategoryGroupPhoto()
        {
            IEnumerator<CategoryGroupPhoto> dataSet = FindAll<CategoryGroupPhoto>("SELECT * FROM Master.CategoryGroupPhotos");

            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;

            }
        }


        public async Task<IEnumerable<CategoryGroupPhoto>> FindAllCategoryGroupPhotoAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Master.CategoryGroupPhotos;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };

            IAsyncEnumerator<CategoryGroupPhoto> dataSet = FindAllAsync<CategoryGroupPhoto>(model);

            var item = new List<CategoryGroupPhoto>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;

        }


        public CategoryGroupPhoto FindOrderById(int id)
        {
            throw new NotImplementedException();
        }





        public CategoryGroup FindCategoryGroupPhotoById(int cagroId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.CategoryGroupPhotos where cagro_id=@cagroId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagroId",
                        DataType = DbType.Int32,
                        Value = cagroId
                    }
                }
            };

            var dataSet = FindByCondition<CategoryGroup>(model);

            var item = new CategoryGroup();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;

        }





        public void Insert(CategoryGroupPhoto categoryGroupPhoto)
        {
                SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO master.CategoryGroupPhotos
                (PhotoFilename,PhotoFileSize,PhotoFileType,PhotoCategoryGroupId,PhotoPrimary,PhotoOriginalFilename) 
                values (@PhotoFilename,@PhotoFileSize,@PhotoFileType,@PhotoCategoryGroupId,@PhotoPrimary,@PhotoOriginalFilename);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFilename",
                        DataType = DbType.String,
                        Value = categoryGroupPhoto.PhotoFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFileSize",
                        DataType = DbType.Int64,
                        Value = categoryGroupPhoto.PhotoFileSize
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoFileType",
                        DataType = DbType.String,
                        Value = categoryGroupPhoto.PhotoFileType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoCategoryGroupId",
                        DataType = DbType.Int64,
                        Value = categoryGroupPhoto.PhotoCategoryGroupId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoPrimary",
                        DataType = DbType.Int64,
                        Value = categoryGroupPhoto.PhotoPrimary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@PhotoOriginalFilename",
                        DataType = DbType.String,
                        Value = categoryGroupPhoto.PhotoOriginalFilename
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }



        public void Remove(CategoryGroupPhoto categoryGroupPhoto)
        {
            throw new NotImplementedException();
        }
    }
}
