using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Product.DataAccess
{
    public class Product:GeneralDataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["Resto_POS"]?.ConnectionString;
        public async Task<List<Models.Product>> GetAll(bool forceGet = false)
        {
            string sqlSyntax = (forceGet) ? "Select * from products" : "Select * from products Where DeletedAt IS NULL Order By ProductId desc";
            return await LoadData<Models.Product, dynamic>(sqlSyntax, new { }, connString);
        }
        public async Task<List<Models.Product>> GetAllWithCategory(bool forceGet = false)
        {
            string sqlSyntax = @"Select p.*,c.* from products p 
                                        inner join categories c on p.CategoryId = c.CategoryId
                                        Where p.deletedAt IS NULL AND c.deletedAt IS NULL Order By p.ProductId desc";
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var datas = await conn.QueryAsync<Models.Product, Models.Category, Models.Product>
                            (sqlSyntax, (product, category) => { product.Category = category; return product;},splitOn:"CategoryId");
                return datas.ToList();
            }
        }
        public async Task<Models.Product> GetByIdWithCategory(int productId,bool forceGet = false)
        {
            var p = new { ProductId = productId };
            string sqlSyntax = @"Select p.*,c.* from products p 
                                        inner join categories c on p.CategoryId = c.CategoryId
                                        Where p.ProductId=@ProductId p.deletedAt IS NULL AND c.deletedAt IS NULL";
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var datas = await conn.QueryAsync<Models.Product, Models.Category, Models.Product>
                            (sqlSyntax, (product, category) => { product.Category = category; return product; },p);
                return datas?.FirstOrDefault();
            }
        }
        public async Task<Models.Product> GetById(int productId, bool forceGet = false)
        {
            var p = new { ProductId = productId };
            string sqlSyntax = (forceGet) ? @"Select * from products Where ProductId=@ProductId" : @"Select * from products Where ProductId=@ProductId And DeletedAt IS NULL";
            var result = await LoadData<Models.Product, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<bool> UpdateProduct(Models.Product product)
        {
            var p = product;
            string sqlSyntax = @"Update products Set ProductName=@ProductName,ProductDescription=@ProductDescription,CategoryId=@CategoryId,
                                    Quantity=@Quantity,Image=@Image,ImageType=@ImageType,ImageName=@ImageName,isAvailable=@isAvailable,updatedAt=@UpdatedAt Where ProductId=@ProductId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> AddProduct(Models.Product product)
        {
            var p = product;
            string sqlSyntax = @"Insert Into products(ProductName,ProductDescription,CategoryId,isAvailable,Quantity,Image,ImageType,ImageName,createdAt,updatedAt)
                                    VALUES(@ProductName,@ProductDescription,@CategoryId,@isAvailable,@Quantity,@Image,@ImageType,@ImageName,@CreatedAt,@UpdatedAt)";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> RemoveProduct(Models.Product product)
        {
            var p = product;
            string sqlSyntax = @"Update products Set deletedAt=@DeletedAt Where ProductId=@ProductId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
    }
}
