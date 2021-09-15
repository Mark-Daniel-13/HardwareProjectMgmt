using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Projects.Dataccess
{
    public class ProjectItems:GeneralDataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["App_DB"]?.ConnectionString;
        public async Task<List<Models.ProjectItems>> GetAll(bool forceGet = false)
        {
            string sqlSyntax = (forceGet) ? "Select * from projectitems" : "Select * from projectitems Where DeletedAt IS NULL Order By ProjectItemId desc";
            return await LoadData<Models.ProjectItems, dynamic>(sqlSyntax, new { }, connString);
        }
        public async Task<List<Models.ProjectItems>> GetByProjectIdWithInventory(int projectId)
        {
            try
            {
                var p = new { ProjectId = projectId };
                string sqlSyntax = @"Select pi.*,i.* from projectitems pi
                                        inner join inventories i on pi.ItemId = i.InventoryId 
                                        Where pi.ProjectId=@ProjectId And pi.DeletedAt IS NULL AND i.deletedAt IS NULL Order By pi.ProjectItemId desc";
                using (IDbConnection conn = new MySqlConnection(connString))
                {
                    var datas = await conn.QueryAsync<Models.ProjectItems,Product.Models.Inventory , Models.ProjectItems>
                                (sqlSyntax, (pi, i) => { pi.Item = i;return pi; }, p,splitOn:"InventoryId");
                    return datas.ToList();
                }
            }
            catch (Exception e)
            {
                var debug = e.Message;
                return null;
            }
        }
        public async Task<Models.ProjectItems> GetById(int projectItemId, bool forceGet = false)
        {
            var p = new { ProjectItemId = projectItemId };
            string sqlSyntax = (forceGet) ? @"Select * from projectitems Where ProjectItemId=@ProjectItemId" : @"Select * from projectitems Where ProjectItemId=@ProjectItemId And DeletedAt IS NULL";
            var result = await LoadData<Models.ProjectItems, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<Models.ProjectItems> GetByIdWithInventory(int projectItemId)
        {
            try
            {
                var p = new { ProjectItemId = projectItemId };
                string sqlSyntax = @"Select pi.*,i.* from projectitems pi
                                        inner join inventories i on pi.ItemId = i.InventoryId 
                                        Where pi.ProjectItemId=@ProjectItemId And pi.DeletedAt IS NULL AND i.deletedAt IS NULL";
                using (IDbConnection conn = new MySqlConnection(connString))
                {
                    var datas = await conn.QueryAsync<Models.ProjectItems, Product.Models.Inventory, Models.ProjectItems>
                                (sqlSyntax, (pi, i) => { pi.Item = i; return pi; }, p,splitOn:"InventoryId");
                    return datas?.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var debug = e.Message;
                return null;
            }
        }
        public async Task<bool> Update(Models.ProjectItems projectItem)
        {
            var p = projectItem;
            string sqlSyntax = @"Update projectitems Set RequiredQty=@RequiredQty,updatedAt=@UpdatedAt Where ProjectItemId=@ProjectItemId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> Add(Models.ProjectItems projectItem)
        {
            var p = projectItem;
            string sqlSyntax = @"Insert Into projectitems(ProjectId,ItemId,RequiredQty,RemainingQty,createdAt,updatedAt)
                                    VALUES(@ProjectId,@ItemId,@RequiredQty,@RemainingQty,@CreatedAt,@UpdatedAt)";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> Remove(Models.ProjectItems projectItem)
        {
            var p = projectItem;
            string sqlSyntax = @"Update projectitems Set deletedAt=@DeletedAt Where ProjectItemId=@ProjectItemId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
    }
}
