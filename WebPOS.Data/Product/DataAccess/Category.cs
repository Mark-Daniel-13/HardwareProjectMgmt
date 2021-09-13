using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Product.DataAccess
{
    public class Category : GeneralDataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["App_DB"]?.ConnectionString;
        public async Task<List<Models.Category>> GetAll(bool forceGet = false)
        {
            string sqlSyntax = (forceGet) ? "Select * from categories" : "Select * from categories Where DeletedAt IS NULL Order By CategoryId desc";
            return await LoadData<Models.Category, dynamic>(sqlSyntax, new { }, connString);
        }

        public async Task<Models.Category> GetById(int categoryId, bool forceGet = false)
        {
            var p = new { CategoryId = categoryId };
            string sqlSyntax = (forceGet) ? @"Select * from categories Where CategoryId=@CategoryId" : @"Select * from categories Where CategoryId=@CategoryId And DeletedAt IS NULL";
            var result = await LoadData<Models.Category, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<bool> UpdateCategory(Models.Category category)
        {
            var p = category;
            string sqlSyntax = @"Update categories Set Name=@Name,updatedAt=@UpdatedAt Where CategoryId=@CategoryId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> AddCategory(Models.Category category)
        {
            var p = category;
            string sqlSyntax = @"Insert Into categories(Name,createdAt,updatedAt) VALUES(@Name,@CreatedAt,@UpdatedAt)";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> RemoveCategory(Models.Category category)
        {
            var p = category;
            string sqlSyntax = @"Update categories Set deletedAt=@DeletedAt Where CategoryId=@CategoryId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
    }
}
