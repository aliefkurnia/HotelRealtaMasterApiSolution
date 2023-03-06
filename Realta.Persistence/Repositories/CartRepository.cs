using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Realta.Persistence.Repositories
{
    internal class CartRepository : RepositoryBase<CartRepository>, ICartRepository
    {
        public CartRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            SqlCommandModel model = new()
            {
                CommandText = "SELECT c.cart_id AS CartId, "
                              + "s.stock_name AS StockName, "
                              + "vp.vepro_price AS VeproPrice, "
                              + "v.vendor_name AS VendorName, "
                              + "c.cart_order_qty AS CartOrderQty, "
                              + "c.cart_modified_date AS CartModifiedDate, "
                              + "c.cart_vepro_id AS CartVeproId, "
                              + "c.cart_emp_id AS CartEmpId, "
                              + "vp.vepro_vendor_id AS VendorId, "
                              + "vp.venpro_stock_id AS StockId "
                              + "FROM purchasing.cart AS c "
                              + "JOIN hr.employee AS e ON e.emp_id = c.cart_emp_id "
                              + "JOIN purchasing.vendor_product AS vp ON vp.vepro_id = c.cart_vepro_id "
                              + "JOIN purchasing.stocks AS s ON s.stock_id = vp.venpro_stock_id "
                              + "JOIN purchasing.vendor AS v ON v.vendor_entity_id = vp.vepro_vendor_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };

            var result = await GetAllAsync<Cart>(model);
            return result;
        }

        public void Insert(Cart cart)
        {
            SqlCommandModel model = new()
            {
                CommandText = "INSERT INTO purchasing.cart (cart_emp_id, cart_vepro_id, cart_order_qty) VALUES (@empId, @veproId, @orderQty)", 
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@veproId",
                        DataType = DbType.Int32,
                        Value = cart.CartVeproId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@empId",
                        DataType = DbType.Int32,
                        Value = cart.CartEmpId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orderQty",
                        DataType = DbType.Int16,
                        Value = cart.CartOrderQty
                    }
                }
            };

            Create(model);
        }

        public void Remove(int id, out bool status)
        {
            status = FindById(id);
            if (status)
            {
                SqlCommandModel model = new()
                {
                    CommandText = "DELETE FROM purchasing.cart WHERE cart_id = @cartId;",
                    CommandType = CommandType.Text,
                    CommandParameters = new SqlCommandParameterModel[] {
                        new SqlCommandParameterModel() {
                            ParameterName = "@cartId",
                            DataType = DbType.Int32,
                            Value = id
                        }
                    }
                };
                Delete(model);
            }
        }

        public void UpdateQty(Cart cart, out bool status)
        {
            status = FindById(cart.CartId);
            if (status)
            {
                SqlCommandModel model = new()
                {
                    CommandText = "UPDATE purchasing.cart "
                                  + "SET cart_order_qty = @orderQty "
                                  + "WHERE cart_id = @cartId",
                    CommandType = CommandType.Text,
                    CommandParameters = new SqlCommandParameterModel[] {
                        new SqlCommandParameterModel() {
                            ParameterName = "@cartId",
                            DataType = DbType.Int32,
                            Value = cart.CartId
                        },
                        new SqlCommandParameterModel() {
                            ParameterName = "@orderQty",
                            DataType = DbType.Int16,
                            Value = cart.CartOrderQty
                        }
                    }
                };

                Update(model);
            }
        }

        bool FindById(int id)
        {

            SqlCommandModel model = new()
            {
                CommandText = "SELECT c.cart_id AS CartId "
                              + "FROM purchasing.cart AS c "
                              + "WHERE cart_id = @cartId",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cartId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Cart>(model);

            Cart? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }

            return (item!=null);
        }
    }
}
